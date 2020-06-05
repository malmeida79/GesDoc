<%@ Page Title="RAD Dimenstein" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GesDoc.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        document.getElementById('fxTela').style.display = 'none';
    </script>

    <div>
        <div class="row">
            <div class="col-md-6">
                <div class="panel panel-default pannelControle">
                    <div class="panel-body text-center">
                        <h2><a href="radioDiag.aspx" style="color: white;">Radiodiagnóstico</a></h2>
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="panel panel-default pannelPlano">
                    <div class="panel-body text-center centered">
                        <h2><a href="medNuclear.aspx" style="color: white;">Medicina Nuclear</a></h2>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
