Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Web.UI
Imports System
Imports System.Web.Security
Imports System.Configuration

Public Class CoreWSL
    Inherits Page

    Private w_user As String = ""
    Private w_usertype As String = ""
    Private w_ip As String = ""
    Private w_sitecode As String = ""
    Private w_location As String = ""
    Private w_role As String = ""
    Private w_username As String = ""
    Private w_pacode As String = ""


    Public Function CoreWSL_Validate(ByVal sHTTPCookie As String) As String
        Dim sErrMsg As String = ""
        Dim sWSLCookie As String = ""
        Dim bValid As Boolean = False

        If sHTTPCookie = "" Then
            sErrMsg = "No cookie!"
        ElseIf ConfigurationSettings.AppSettings("wsl_version") = "4.0" Then
            Dim objValidate As Object = HttpContext.Current.Server.CreateObject("FordWSL.Validate")

            Try
                objValidate.PublicKeys = ConfigurationSettings.AppSettings("wsl_4")
                For Each n As String In Split(sHTTPCookie, ";")
                    Dim aryVal As Array = Split(n, "=", 2)
                    If Trim(aryVal(0)) = "WSLX-credential" Then sWSLCookie = Trim(aryVal(1))
                Next
                With objValidate
                    .Cookie = sWSLCookie
                    bValid = .ValidateCookie
                    If bValid Then
                        Dim sAttNames = .AttributeNames
                        For Each sName As String In sAttNames
                            Dim sVal = UCase(objValidate.Attribute(sName))
                            Select Case LCase(sName)
                                Case "userid"
                                    w_user = sVal
                                Case "acigroup"
                                    w_usertype = sVal
                                Case "ipaddr"
                                    w_ip = sVal
                                Case "dept"
                                    w_sitecode = sVal
                                Case "orgcode"
                                    w_location = sVal
                                Case "empcode"
                                    w_role = sVal
                                Case "mrrole"
                                    w_username = Replace(Replace(sVal, "?", ""), "~", " ")
                                Case "org"
                                    w_pacode = sVal
                            End Select
                        Next
                    Else
                        Dim iReason = .Validity
                        Select Case iReason
                            Case 0
                                sErrMsg = "No cookie!"
                            Case 1
                                sErrMsg = "Cookie is invalid!"
                            Case 2
                                sErrMsg = "Cookie has expired!"
                            Case 3
                                sErrMsg = "Cookie has invalid signature!"
                            Case 4
                                sErrMsg = "Cookie has wrong IP address!!"
                        End Select
                    End If
                End With
            Catch ex As Exception
                Throw (ex)
            Finally
                objValidate = Nothing
            End Try
        Else
            Dim Cookie As String
            Dim X As Integer
            Dim aryCookie As Array = Split(sHTTPCookie, "; ")
            For X = 0 To UBound(aryCookie)
                If InStr(UCase(aryCookie(X)), "WSLX=") > 0 Then
                    Cookie = Right(aryCookie(X), Len(aryCookie(X)) - 5)
                End If
            Next

            Dim l_len As Integer = Len(Cookie)
            Dim l_output_bit As Integer = 0
            Dim l_byte_count As Integer = 0
            Dim l_ascii As Integer = 0
            Dim l_ascii_string As String = ""
            Dim l_last As Boolean = False
            Dim l_field_seperator As String = "\"
            Dim l_index, l_input_bit, l_bit_value, l_next_pos, l_pos As Integer
            Dim l_asc, l_value, l_format_nulls, l_user_id, l_ip_add, l_user_type As String
            Dim l_site_code_type, l_location, l_role, l_name, l_pa_code, l_temp1, l_temp2 As String

            'Parse Cookie
            For l_index = 5 To l_len
                l_asc = Asc(Mid(Cookie, l_index, 1))
                If l_asc = 43 Or l_asc = 32 Then
                    l_value = 62
                ElseIf l_asc = 47 Then
                    l_value = 63
                ElseIf l_asc > 47 And l_asc < 58 Then
                    l_value = l_asc + 4
                ElseIf l_asc > 64 And l_asc < 91 Then
                    l_value = l_asc - 65
                ElseIf l_asc > 96 And l_asc < 123 Then
                    l_value = l_asc - 71
                    '<!---Quitting when = is found (base64 terminator)--->
                ElseIf l_asc = 61 Then
                    l_value = ""
                    Exit For
                Else
                    Exit For
                End If
                '<!---The bits are high value first, so I'll go down from the high one--->
                For l_input_bit = 6 To 1 Step -1
                    '<!---Decrement the output bit, or start on a new 8-bit byte--->
                    If l_output_bit > 1 Then
                        l_output_bit = l_output_bit - 1
                    Else
                        '<!---The current byte is done.  Check to see if null counting is being done, and I'm supposed to start yet--->
                        l_byte_count = l_byte_count + 1
                        '!---Append this byte to the output string, and set up for the next byte--->
                        If l_format_nulls = "Y" And l_ascii = 0 Then
                            l_ascii_string = l_ascii_string & "<BR>"
                        Else
                            If l_ascii = 0 Then
                                l_ascii_string = l_ascii_string & l_field_seperator
                            Else
                                l_ascii_string = l_ascii_string & Chr(l_ascii)
                            End If
                        End If
                        l_output_bit = 8
                        l_ascii = 0
                    End If
                    '<!---Get the bit value--->
                    If l_input_bit = 1 Then
                        l_bit_value = 1
                    Else
                        l_bit_value = 2 ^ (l_input_bit - 1)
                    End If
                    '<!---See if this bit is set--->
                    If l_value >= l_bit_value Then
                        '<!---Set the current bit in the 8-bit number--->
                        If l_output_bit = 1 Then
                            l_ascii = l_ascii + 1
                        Else
                            l_ascii = l_ascii + 2 ^ (l_output_bit - 1)
                        End If
                        '<!---Turn the bit off in the 6-bit number, so I can check for the lower bits next time--->
                        l_value = l_value - l_bit_value
                    End If
                Next
            Next

            l_pos = InStr(1, l_ascii_string, "dlrconn")
            l_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            l_next_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            l_user_id = Mid(l_ascii_string, l_pos + 1, l_next_pos - l_pos - 1)

            l_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            l_next_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            l_ip_add = Mid(l_ascii_string, l_pos + 1, l_next_pos - l_pos - 1)
            '<!---User type--->
            l_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            l_next_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            If l_pos > 0 And l_next_pos = 0 And Not l_last Then
                l_next_pos = l_byte_count + 1
                l_last = True
            End If
            If l_next_pos > 0 Then
                l_user_type = Mid(l_ascii_string, l_pos + 1, l_next_pos - l_pos - 1)
            Else
                l_user_type = ""
            End If
            'Site Code Type
            l_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            l_next_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            If l_pos > 0 And l_next_pos = 0 And Not l_last Then
                l_next_pos = l_byte_count + 1
                l_last = True
            End If
            If l_next_pos > 0 Then
                l_site_code_type = Mid(l_ascii_string, l_pos + 1, l_next_pos - l_pos - 1)
            Else
                l_site_code_type = ""
            End If
            'Response.Write "<br> Site Code Type: "&l_site_code_type
            'Location
            l_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            l_next_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            If l_pos > 0 And l_next_pos = 0 And Not l_last Then
                l_next_pos = l_byte_count + 1
                l_last = True
            End If
            If l_next_pos > 0 Then
                l_location = Mid(l_ascii_string, l_pos + 1, l_next_pos - l_pos - 1)
            Else
                l_location = ""
            End If

            ' Role
            l_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            l_next_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            If l_pos > 0 And l_next_pos = 0 And Not l_last Then
                l_next_pos = l_byte_count + 1
                l_last = True
            End If
            If l_next_pos > 0 Then
                l_role = Mid(l_ascii_string, l_pos + 1, l_next_pos - l_pos - 1)
            Else
                l_role = ""
            End If
            'Response.Write "<br> Role: "&l_role
            'User Name
            l_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            l_next_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            If l_pos > 0 And l_next_pos = 0 And Not l_last Then
                l_next_pos = l_byte_count + 1
                l_last = True
            End If
            If l_next_pos > 0 Then
                l_name = Mid(l_ascii_string, l_pos + 1, l_next_pos - l_pos - 1)
            Else
                l_name = ""
            End If
            l_name = Replace(l_name, "~", " ")
            l_name = Replace(l_name, "~", "")
            'Response.Write "<br> User Name: "&l_name

            'PACODE / Supplier Code
            l_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            l_next_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            If l_pos > 0 And l_next_pos = 0 And Not l_last Then
                l_next_pos = l_byte_count + 1
                l_last = True
            End If
            If l_next_pos > 0 Then
                l_pa_code = Mid(l_ascii_string, l_pos + 1, l_next_pos - l_pos - 1)
            Else
                l_pa_code = ""
            End If
            'Response.Write "<br> PACode: "&l_pa_code
            ' Temp1
            l_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            l_next_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            If l_pos > 0 And l_next_pos = 0 And Not l_last Then
                l_next_pos = l_byte_count + 1
                l_last = True
            End If
            If l_next_pos > 0 Then
                l_temp1 = Mid(l_ascii_string, l_pos + 1, l_next_pos - l_pos - 1)
            Else
                l_temp1 = ""
            End If
            'Response.Write "<br> Temp1: "&l_temp1
            ' Temp2
            l_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            l_next_pos = InStr(l_pos + 1, l_ascii_string, l_field_seperator)
            If l_pos > 0 And l_next_pos = 0 And Not l_last Then
                l_next_pos = l_byte_count + 1
                l_last = True
            End If
            If l_next_pos > 0 Then
                l_temp2 = Mid(l_ascii_string, l_pos + 1, l_next_pos - l_pos - 1)
            Else
                l_temp2 = ""
            End If

            w_user = l_user_id
            w_ip = l_ip_add
            w_usertype = l_user_type
            w_sitecode = l_site_code_type
            w_location = l_location
            w_role = l_role
            w_username = l_name
            w_pacode = l_pa_code
        End If
        If w_user.Length = 0 Then sErrMsg = "WSL ID cannot be null!"
        If w_pacode.Length = 0 Then sErrMsg = "PACode cannot be null!"
        'Added a check to avoid session setting
        If sErrMsg = "" And Session("w_pacode") Is Nothing Then Call SetWSLSession()
        Return (sErrMsg)
    End Function

    Private Sub SetWSLSession()
        Session("w_user") = w_user
        Session("w_usertype") = w_usertype
        Session("w_ip") = w_ip
        Session("w_sitecode") = w_sitecode
        Session("w_location") = w_location
        Session("w_role") = w_role
        Session("w_username") = w_username
        Session("w_pacode") = w_pacode
    End Sub

    Private Sub setCookie(ByVal cookiename As String, ByVal cookievalue As String)
        Dim aCookie As New HttpCookie(cookiename)
        aCookie.Value = EncryptedCookieData(cookievalue, cookiename)
        HttpContext.Current.Response.Cookies.Add(aCookie)
    End Sub

    Private Function EncryptedCookieData(ByVal cookieValue As String, ByVal cookieName As String)
        Dim returnString As String = ""
        Dim ticket As New FormsAuthenticationTicket(1, cookieName, DateTime.Now, DateTime.Now.AddMinutes(30), False, cookieValue, "/")
        returnString = FormsAuthentication.Encrypt(ticket)
        Return returnString
    End Function

End Class
