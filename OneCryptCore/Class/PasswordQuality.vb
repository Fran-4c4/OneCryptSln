

Imports System.Text.RegularExpressions

Namespace ind.crypt
    Public Class PasswordQuality

        ''' <summary>Determines if a password is sufficiently complex.</summary> 
        ''' <param name="pwd">Password to validate</param> 
        ''' <param name="minLength">Minimum number of password characters.</param> 
        ''' <param name="numUpper">Minimum number of uppercase characters.</param> 
        ''' <param name="numLower">Minimum number of lowercase characters.</param> 
        ''' <param name="numNumbers">Minimum number of numeric characters.</param> 
        ''' <param name="numSpecial">Minimum number of special characters.</param> 
        ''' <returns>True if the password is sufficiently complex.</returns> 
        Public Shared Function ValidatePasswordQuality(ByVal pwd As String,
            Optional ByVal minLength As Integer = 8,
            Optional ByVal numUpper As Integer = 2,
            Optional ByVal numLower As Integer = 2,
            Optional ByVal numNumbers As Integer = 2,
            Optional ByVal numSpecial As Integer = 2) As Boolean

            pwd = pwd.Trim

            ' Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters. 
            Dim upper As New System.Text.RegularExpressions.Regex("[A-Z]")
            Dim lower As New System.Text.RegularExpressions.Regex("[a-z]")
            Dim number As New System.Text.RegularExpressions.Regex("[0-9]")
            ' Special is "none of the above". 
            Dim special As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]")

            ' Check the length. 
            If Len(pwd) < minLength Then Return False
            ' Check for minimum number of occurrences. 
            If upper.Matches(pwd).Count < numUpper Then Return False
            If lower.Matches(pwd).Count < numLower Then Return False
            If number.Matches(pwd).Count < numNumbers Then Return False
            If special.Matches(pwd).Count < numSpecial Then Return False

            ' Passed all checks. 
            Return True
        End Function

        Public Enum PasswordScore
            Blank = 0
            VeryWeak = 1
            Weak = 2
            Medium = 3
            GOOD = 4
            Strong = 5
            VeryStrong = 6
        End Enum


        Public Function CheckStrength(Optional typeCheck As Integer = 0) As Integer
            If typeCheck = 0 Then
                Return CheckStrength(Me.PreviousPassword)
            Else
                Return CheckStrength2(Me.PreviousPassword)
            End If

        End Function
        ''' <summary>
        ''' check the strength of password
        ''' </summary>
        ''' <param name="password"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CheckStrength(ByVal password As String) As PasswordScore
            Dim score As Integer = 1

            'Return PasswordScore.VeryStrong

            If password.Length < 1 Then
                Return PasswordScore.Blank
            End If
            If password.Length < 4 Then
                Return PasswordScore.VeryWeak
            End If

            If password.Length >= 5 Then
                score = score + 1
            End If
            If password.Length >= 10 Then
                score = score + 1
            End If

            If Regex.IsMatch(password, "\d+") Then
                score = score + 1
            End If

            If Regex.IsMatch(password, "[a-z]") Then

                score = score + 1
            End If

            If Regex.IsMatch(password, "[0-9]") Then

                score = score + 1
            End If

            If Regex.IsMatch(password, "[A-Z]") Then
                score = score + 1
            End If
            If Regex.IsMatch(password, "[.,!,@,#,$,%,^,&]") Then
                score = score + 1
            End If
            If Regex.IsMatch(password, "[*,?,_,~,-,£,(,)]") Then
                score = score + 1
            End If

            Return CType(score, PasswordScore)
        End Function

        Private PreviousPassword As String = ""

        Public Sub SetPassword(pwd As String)
            If PreviousPassword <> pwd Then
                PreviousPassword = pwd
            End If

        End Sub


        Public Function CheckStrength2(pwd As String) As Integer
            ' Init Vars
            Dim nScore As Integer = 0
            Dim sComplexity As String = ""
            Dim iUpperCase As Integer = 0
            Dim iLowerCase As Integer = 0
            Dim iDigit As Integer = 0
            Dim iSymbol As Integer = 0
            Dim iRepeated As Integer = 1
            Dim htRepeated As New Hashtable()
            Dim iMiddle As Integer = 0
            Dim iMiddleEx As Integer = 1
            Dim ConsecutiveMode As Integer = 0
            Dim iConsecutiveUpper As Integer = 0
            Dim iConsecutiveLower As Integer = 0
            Dim iConsecutiveDigit As Integer = 0
            Dim iLevel As Integer = 0
            Dim sAlphas As String = "abcdefghijklmnopqrstuvwxyz"
            Dim sNumerics As String = "01234567890"
            Dim nSeqAlpha As Integer = 0
            Dim nSeqChar As Integer = 0
            Dim nSeqNumber As Integer = 0

            ' Create data table to store results
            'CreateDetailsTable()
            'Dim drScore As DataRow = AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Score", "", "", 0, 0)

            ' Scan password
            For Each ch As Char In pwd.ToCharArray()
                ' Count digits
                If [Char].IsDigit(ch) Then
                    iDigit += 1

                    If ConsecutiveMode = 3 Then
                        iConsecutiveDigit += 1
                    End If
                    ConsecutiveMode = 3
                End If

                ' Count uppercase characters
                If [Char].IsUpper(ch) Then
                    iUpperCase += 1
                    If ConsecutiveMode = 1 Then
                        iConsecutiveUpper += 1
                    End If
                    ConsecutiveMode = 1
                End If

                ' Count lowercase characters
                If [Char].IsLower(ch) Then
                    iLowerCase += 1
                    If ConsecutiveMode = 2 Then
                        iConsecutiveLower += 1
                    End If
                    ConsecutiveMode = 2
                End If

                ' Count symbols
                If [Char].IsSymbol(ch) OrElse [Char].IsPunctuation(ch) Then
                    iSymbol += 1
                    ConsecutiveMode = 0
                End If

                ' Count repeated letters 
                If [Char].IsLetter(ch) Then
                    If htRepeated.Contains([Char].ToLower(ch)) Then
                        iRepeated += 1
                    Else
                        htRepeated.Add([Char].ToLower(ch), 0)
                    End If

                    If iMiddleEx > 1 Then
                        iMiddle = iMiddleEx - 1
                    End If
                End If

                If iUpperCase > 0 OrElse iLowerCase > 0 Then
                    If [Char].IsDigit(ch) OrElse [Char].IsSymbol(ch) Then
                        iMiddleEx += 1
                    End If
                End If
            Next

            ' Check for sequential alpha string patterns (forward and reverse) 
            For s As Integer = 0 To 22
                Dim sFwd As String = sAlphas.Substring(s, 3)
                Dim sRev As String = StrReverse(sFwd)
                If pwd.ToLower().IndexOf(sFwd) <> -1 OrElse pwd.ToLower().IndexOf(sRev) <> -1 Then
                    nSeqAlpha += 1
                    nSeqChar += 1
                End If
            Next

            ' Check for sequential numeric string patterns (forward and reverse)
            For s As Integer = 0 To 7
                Dim sFwd As String = sNumerics.Substring(s, 3)
                Dim sRev As String = StrReverse(sFwd)
                If pwd.ToLower().IndexOf(sFwd) <> -1 OrElse pwd.ToLower().IndexOf(sRev) <> -1 Then
                    nSeqNumber += 1
                    nSeqChar += 1
                End If
            Next

            ' Calcuate score
            'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Additions", "", "", 0, 0)

            ' Score += 4 * Password Length
            nScore = 4 * pwd.Length
            'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Password Length", "Flat", "(n*4)", pwd.Length, pwd.Length * 4)

            ' if we have uppercase letetrs Score +=(number of uppercase letters *2)
            If iUpperCase > 0 Then
                nScore += ((pwd.Length - iUpperCase) * 2)
                'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Uppercase Letters", "Cond/Incr", "+((len-n)*2)", iUpperCase, ((pwd.Length - iUpperCase) * 2))
            Else
                'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Uppercase Letters", "Cond/Incr", "+((len-n)*2)", iUpperCase, 0)
            End If

            ' if we have lowercase letetrs Score +=(number of lowercase letters *2)
            If iLowerCase > 0 Then
                nScore += ((pwd.Length - iLowerCase) * 2)
                'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Lowercase Letters", "Cond/Incr", "+((len-n)*2)", iLowerCase, ((pwd.Length - iLowerCase) * 2))
            Else
                'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Lowercase Letters", "Cond/Incr", "+((len-n)*2)", iLowerCase, 0)
            End If


            ' Score += (Number of digits *4)
            nScore += (iDigit * 4)
            'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Numbers", "Cond", "+(n*4)", iDigit, (iDigit * 4))

            ' Score += (Number of Symbols * 6)
            nScore += (iSymbol * 6)
            'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Symbols", "Flat", "+(n*6)", iSymbol, (iSymbol * 6))

            ' Score += (Number of digits or symbols in middle of password *2)
            nScore += (iMiddle * 2)
            'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Middle Numbers or Symbols", "Flat", "+(n*2)", iMiddle, (iMiddle * 2))

            'requirments
            Dim requirments As Integer = 0
            If pwd.Length >= 8 Then
                requirments += 1
            End If
            ' Min password length
            If iUpperCase > 0 Then
                requirments += 1
            End If
            ' Uppercase letters
            If iLowerCase > 0 Then
                requirments += 1
            End If
            ' Lowercase letters
            If iDigit > 0 Then
                requirments += 1
            End If
            ' Digits
            If iSymbol > 0 Then
                requirments += 1
            End If
            ' Symbols
            ' If we have more than 3 requirments then
            If requirments > 3 Then
                ' Score += (requirments *2) 
                nScore += (requirments * 2)
                'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Requirments", "Flat", "+(n*2)", requirments, (requirments * 2))
            Else
                'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Requirments", "Flat", "+(n*2)", requirments, 0)
            End If

            '
            ' Deductions
            '
            'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Deductions", "", "", 0, 0)

            ' If only letters then score -=  password length
            If iDigit = 0 AndAlso iSymbol = 0 Then
                nScore -= pwd.Length
                'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Letters only", "Flat", "-n", pwd.Length, -pwd.Length)
            Else
                'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Letters only", "Flat", "-n", 0, 0)
            End If

            ' If only digits then score -=  password length
            If iDigit = pwd.Length Then
                nScore -= pwd.Length
                'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Numbers only", "Flat", "-n", pwd.Length, -pwd.Length)
            Else
                'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Numbers only", "Flat", "-n", 0, 0)
            End If

            ' If repeated letters used then score -= (iRepeated * (iRepeated - 1));
            If iRepeated > 1 Then
                nScore -= (iRepeated * (iRepeated - 1))
                'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Repeat Characters (Case Insensitive)", "Incr", "-(n(n-1))", iRepeated, -(iRepeated * (iRepeated - 1)))
            End If

            ' If Consecutive uppercase letters then score -= (iConsecutiveUpper * 2);
            nScore -= (iConsecutiveUpper * 2)
            'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Consecutive Uppercase Letters", "Flat", "-(n*2)", iConsecutiveUpper, -(iConsecutiveUpper * 2))

            ' If Consecutive lowercase letters then score -= (iConsecutiveUpper * 2);
            nScore -= (iConsecutiveLower * 2)
            'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Consecutive Lowercase Letters", "Flat", "-(n*2)", iConsecutiveLower, -(iConsecutiveLower * 2))

            ' If Consecutive digits used then score -= (iConsecutiveDigits* 2);
            nScore -= (iConsecutiveDigit * 2)
            'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Consecutive Numbers", "Flat", "-(n*2)", iConsecutiveDigit, -(iConsecutiveDigit * 2))

            ' If password contains sequence of letters then score -= (nSeqAlpha * 3)
            nScore -= (nSeqAlpha * 3)
            'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Sequential Letters (3+)", "Flat", "-(n*3)", nSeqAlpha, -(nSeqAlpha * 3))

            ' If password contains sequence of digits then score -= (nSeqNumber * 3)
            nScore -= (nSeqNumber * 3)
            'AddDetailsRow(System.Math.Max(System.Threading.Interlocked.Increment(iLevel), iLevel - 1), "Sequential Numbers (3+)", "Flat", "-(n*3)", nSeqNumber, -(nSeqNumber * 3))

            ' Determine complexity based on overall score 

            If nScore > 100 Then
                nScore = 100
            ElseIf nScore < 0 Then
                nScore = 0
            End If
            If nScore >= 0 AndAlso nScore < 20 Then
                sComplexity = "Very Weak"
            ElseIf nScore >= 20 AndAlso nScore < 40 Then
                sComplexity = "Weak"
            ElseIf nScore >= 40 AndAlso nScore < 60 Then
                sComplexity = "Good"
            ElseIf nScore >= 60 AndAlso nScore < 80 Then
                sComplexity = "Strong"
            ElseIf nScore >= 80 AndAlso nScore <= 100 Then
                sComplexity = "Very Strong"
            End If

            ' Store score and complexity in dataset
            'drScore(5) = nScore
            'drScore(3) = sComplexity
            'dtDetails.AcceptChanges()

            Return nScore
        End Function



    End Class


End Namespace


