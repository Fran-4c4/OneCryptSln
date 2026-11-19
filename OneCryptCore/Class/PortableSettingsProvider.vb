Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text
Imports System.Configuration
Imports System.Configuration.Provider
Imports System.Windows.Forms
Imports System.Collections.Specialized
Imports Microsoft.Win32
Imports System.Xml
Imports System.Reflection


Public Class PortableSettingsProvider

    Const SETTINGSROOT As String = "Settings" 'XML Root Node

    Public Sub New()

    End Sub

    Public Property ApplicationName() As String
        Get
            If Application.ProductName.Trim.Length > 0 Then
                Return Application.ProductName
            Else
                Dim fi As New IO.FileInfo(Application.ExecutablePath)
                Return fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length)
            End If
        End Get
        Set(ByVal value As String)
            'Do nothing
        End Set
    End Property
    ''' <summary>
    ''' Get the folder where DLL is stored, it is diferent from executable path.
    ''' Why this? Because DLL and executable are in the same path but when windows explorer open using shell the path is windows and do not find de configuration file.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overridable Function GetAppSettingsPath() As String
        'Used to determine where to store the settings
        Dim appPath As String = My.Application.Info.DirectoryPath
        Return appPath
    End Function
    ''' <summary>
    ''' File name where to save the settings
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Overridable Function GetAppSettingsFilename() As String
        'Used to determine the filename to store the settings
        Return ApplicationName & ".settings"
    End Function

    Public Sub SetPropertyValues(ByVal props As Reflection.PropertyInfo())
        'Iterate through the settings to be stored
        'Only dirty settings are included in propvals, and only ones relevant to this provider
        For Each propval As PropertyInfo In props
            SetValue(propval)
        Next

        Try
            SettingsXML.Save(IO.Path.Combine(GetAppSettingsPath, GetAppSettingsFilename))
        Catch ex As Exception
            'Ignore if cant save, device been ejected
        End Try
    End Sub

    Public Sub GetPropertyValues(ByVal props As Reflection.PropertyInfo())

        'Iterate through the settings to be retrieved
        For Each setting As PropertyInfo In props
            Dim propertyVal As Object = GetValue(setting.Name)
            If Not propertyVal Is Nothing Then
                'find the property type
                Dim propertyType As Type = setting.PropertyType

                '//Returns an System.Object with the specified System.Type and whose value is
                '//equivalent to the specified object.
                propertyVal = Convert.ChangeType(propertyVal, propertyType)

                '//Set the value of the property
                setting.SetValue(Me, propertyVal, Nothing)
            End If

        Next

    End Sub

    Private m_SettingsXML As Xml.XmlDocument = Nothing

    Private ReadOnly Property SettingsXML() As Xml.XmlDocument
        Get
            'If we dont hold an xml document, try opening one.  
            'If it doesnt existObj then create a new one ready.
            If m_SettingsXML Is Nothing Then
                m_SettingsXML = New Xml.XmlDocument

                Dim filename As String = IO.Path.Combine(GetAppSettingsPath, GetAppSettingsFilename)

                If IO.File.Exists(filename) = True Then
                    m_SettingsXML.Load(filename)
                    'check if settings node exists
                    Dim nodeRoot As XmlNode
                    nodeRoot = DirectCast(m_SettingsXML.SelectSingleNode(SETTINGSROOT), XmlElement)
                    If nodeRoot Is Nothing Then
                        nodeRoot = m_SettingsXML.CreateNode(XmlNodeType.Element, SETTINGSROOT, "")
                        m_SettingsXML.AppendChild(nodeRoot)
                    End If
                    m_SettingsXML.Save(filename)
                Else
                    'create file
                    'Create new document
                    Dim dec As XmlDeclaration = m_SettingsXML.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
                    m_SettingsXML.AppendChild(dec)

                    Dim nodeRoot As XmlNode

                    nodeRoot = m_SettingsXML.CreateNode(XmlNodeType.Element, SETTINGSROOT, "")
                    m_SettingsXML.AppendChild(nodeRoot)

                End If

                'Try
                '    m_SettingsXML.Load(filename)
                'Catch ex As Exception
                '    'Create new document
                '    Dim dec As XmlDeclaration = m_SettingsXML.CreateXmlDeclaration("1.0", "utf-8", String.Empty)
                '    m_SettingsXML.AppendChild(dec)

                '    Dim nodeRoot As XmlNode

                '    nodeRoot = m_SettingsXML.CreateNode(XmlNodeType.Element, SETTINGSROOT, "")
                '    m_SettingsXML.AppendChild(nodeRoot)
                'End Try
            End If

            Return m_SettingsXML
        End Get
    End Property

    Private Function GetValue(ByVal settingName As String) As Object
        Dim ret As Object = Nothing

        Try
            Dim n As Xml.XmlNode = SettingsXML.SelectSingleNode(SETTINGSROOT & "/" & settingName)
            If Not n Is Nothing Then
                ret = n.InnerText
            Else
                ret = Nothing
            End If

        Catch ex As Exception

        End Try

        Return ret
    End Function

    Private Sub SetValue(ByVal propVal As PropertyInfo)

        Dim SettingNode As Xml.XmlElement


        Try
            SettingNode = DirectCast(SettingsXML.SelectSingleNode(SETTINGSROOT & "/" & propVal.Name), XmlElement)

        Catch ex As Exception
            SettingNode = Nothing
        End Try

        Dim propValue As Object = propVal.GetValue(Me, Nothing)
        Dim serializedValue As String = CStr(propValue)
        'Check to see if the node exists, if so then set its new value
        If Not SettingNode Is Nothing Then
            SettingNode.InnerText = serializedValue
        Else

            'Store the value as an element of the Settings Root Node
            SettingNode = SettingsXML.CreateElement(propVal.Name)
            SettingNode.InnerText = serializedValue
            SettingsXML.SelectSingleNode(SETTINGSROOT).AppendChild(SettingNode)


        End If
    End Sub

    Private Function IsRoaming(ByVal prop As SettingsProperty) As Boolean
        'Determine if the setting is marked as Roaming
        For Each d As DictionaryEntry In prop.Attributes
            Dim a As Attribute = DirectCast(d.Value, Attribute)
            If TypeOf a Is System.Configuration.SettingsManageabilityAttribute Then
                Return True
            End If
        Next
        Return False
    End Function


End Class
