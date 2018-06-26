<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication3.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            background-color:aliceblue;
        }
        .allDoc {
            text-align:center;
        }
        .words {
            font-family:'Times New Roman', Times, serif;
            color:green;
            font-size:15px;
        }
        .title {
            text-align:center;
        }
    </style>
</head>
<body>
    <h1 class ="title"> Sentence parser </h1>
    <form  id="form1" runat="server" class ="allDoc">
      
        &nbsp;<asp:FileUpload ID="fileSentences" runat="server" Height="41px" style="margin-left: 0px" />
        <asp:Button ID="uploadButton" runat="server" OnClick="uploadButton_Click" Text="Upload" Height="41px" style="margin-top: 0px" Width="98px" />
        <br /><br/>
&nbsp;<asp:TextBox ID="TextSearch" runat="server" OnTextChanged="TextSearch_TextChanged" Width="128px" Height="35px"></asp:TextBox>
        <asp:Button ID="searchButton" runat="server" Height="41px" OnClick="searchButton_Click" Text="Search" Width="79px" />
       
        <p>
            &nbsp;</p>
        <asp:Label ID="lbloutput" runat="server" class ="words"></asp:Label><br/>
        <asp:Label runat="server" ID="ForSentences" class="words"></asp:Label><br/>
        
    </form>
</body>
</html>
