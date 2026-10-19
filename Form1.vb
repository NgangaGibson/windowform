
Imports MySql.Data.MySqlClient

Public Class Form1

    Dim connectionString As String =
        "Server=localhost;Port=3306;Database=studentdb;Uid=root;Pwd=;"

    Private Sub btnrunbutton_Click(sender As Object, e As EventArgs) Handles btnrunbutton.Click

        Dim studentName As String = TxtName.Text.Trim()
        Dim idNumber As String = TxtIDNO.Text.Trim()
        Dim courseName As String = Txtcoursename.Text.Trim()
        Dim feeBalance As Decimal

        If studentName = "" OrElse idNumber = "" OrElse courseName = "" OrElse Txtfeebalance.Text.Trim() = "" Then
            MessageBox.Show("Please fill in all fields.")
            Return
        End If

        If Not Decimal.TryParse(Txtfeebalance.Text, feeBalance) OrElse feeBalance < 0 Then
            MessageBox.Show("Enter a valid, non-negative fee balance.")
            Return
        End If

        Try
            Using connection As New MySqlConnection(connectionString)

                connection.Open()

                Dim query As String =
                    "INSERT INTO students (student_name, id_number, course_name, fee_balance) " &
                    "VALUES (@studentName, @idNumber, @courseName, @feeBalance)"

                Using command As New MySqlCommand(query, connection)

                    command.Parameters.AddWithValue("@studentName", studentName)
                    command.Parameters.AddWithValue("@idNumber", idNumber)
                    command.Parameters.AddWithValue("@courseName", courseName)
                    command.Parameters.AddWithValue("@feeBalance", feeBalance)

                    command.ExecuteNonQuery()

                End Using

            End Using

            MessageBox.Show("Student record saved successfully!")

            TxtName.Clear()
            TxtIDNO.Clear()
            Txtcoursename.Clear()
            Txtfeebalance.Clear()

        Catch ex As Exception
            MessageBox.Show("Database error: " & ex.Message)
        End Try

    End Sub

End Class
