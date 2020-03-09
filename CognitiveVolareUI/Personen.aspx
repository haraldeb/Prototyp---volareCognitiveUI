<%@ Page Title="Erkennung von Vorarlberger Personen" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Personen.aspx.cs" Inherits="CognitiveVolareUI.Personen" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
        <ProgressTemplate>
            <div class="overlay">
                <div class="centered">
                    <img src="Images/giphy.gif" />
                    <p>Ich suche nach bekannten Personen.</p>
                </div>
            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="Panel">
        <ContentTemplate>
            <p>
                <asp:Label ID="lblVolareObjektId" runat="server" Text="volare-Objekt-ID: (zb. o:87126, o:157301, o:90458, o:128758)"></asp:Label>
            </p>
            <p>
                <asp:TextBox ID="txtVolareObjektId" runat="server" ToolTip="Eindeutige Objekt ID, zum Beispiel o:87126" Width="200px" OnTextChanged="txtVolareObjektId_TextChanged">o:87126</asp:TextBox>
            </p>
            <p>
                <asp:Button ID="UpdateButton" runat="server" OnClick="btnErkennePersonen_Click" Text="Personen erkennen" />
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
