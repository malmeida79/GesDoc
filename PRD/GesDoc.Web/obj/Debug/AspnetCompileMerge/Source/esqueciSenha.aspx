<%@ Page Title="Esqueci senha" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="esqueciSenha.aspx.cs" Inherits="GesDoc.Web.esqueciSenha" %>

<%@ Register Src="~/Controls/ButtonBar.ascx" TagPrefix="uc1" TagName="ButtonBar" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        document.getElementById('fxTela').style.display = 'block';
        document.getElementById('fxTela').innerHTML = 'Esqueci a minha senha';
    </script>

    <br />
    <center>
            <br />
          <table width="100%">
        <tr>
            <td>
                <center><br />
    <table>
        <tr>
            <td align="right" class="auto-style6">Email para lembrar senha: </td>
            <td align="left" class="auto-style6">
                <asp:TextBox ID="txtEmail" runat="server" Width="325px" TabIndex="2" ></asp:TextBox>
            </td>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
            <uc1:ButtonBar runat="server" id="ButtonBar" />

            </td>
          
        </tr>
    </table>
        </center>
            </td>
        </tr>
    </table>
        <br />
        </center>

</asp:Content>
