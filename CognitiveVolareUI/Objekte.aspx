<%@ Page Title="Erkennung von Objekten und Texten" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Objekte.aspx.cs" Inherits="CognitiveVolareUI.Objekte" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
        <ProgressTemplate>
            <div class="overlay">
                <div class="centered">
                    <img src="Images/giphy.gif" />
                    <p>Ich suche nach Objekten und Texten.</p>
                </div>
            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="Panel">
        <ContentTemplate>

            <p>
                <asp:Label ID="lblVolareObjektId" runat="server" Text="volare-Objekt-ID: (zb. o:85449, o:164965, o:44916, o:45324, o:111872, o:113467)"></asp:Label>
            </p>
            <p>
                <asp:TextBox ID="txtVolareObjektId" runat="server" ToolTip="Eindeutige Objekt ID, zum Beispiel o:113467" Width="200px" OnTextChanged="txtVolareObjektId_TextChanged">o:113467</asp:TextBox>
            </p>
            <p>

                <asp:Button ID="btnErkennePersonen" runat="server" OnClick="btnErkenneObjekte_Click" Text="Objekte erkennen" />

            </p>
            <p>
                <asp:Image ID="imgShowVolareImage" runat="server" Visible="False" CssClass="volareimage" />
            </p>
            <p>
                <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:Literal ID="ltContent" runat="server"></asp:Literal>
            </p>

            <p>
                <asp:Label ID="lblEstYear" runat="server" Text=""></asp:Label>
            </p>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
