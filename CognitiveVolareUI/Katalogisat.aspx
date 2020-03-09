<%@ Page Title="Erstellung maschinenlesbarer Metadaten mittels künstlicher Intelligenz im Format MARC21" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Katalogisat.aspx.cs" Inherits="CognitiveVolareUI.Katalogisat" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
        <ProgressTemplate>
            <div class="overlay">
                <div class="centered">
                    <img src="Images/giphy.gif" />
                    <p>Ich erstelle Metadaten in MARC21.</p>
                </div>
            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="Panel">
        <ContentTemplate>
            <p>
                <asp:Label ID="lblVolareObjektId" runat="server" Text="volare-Objekt-ID: (zb. o:128758, o:157301, o:128608, o:63779)"></asp:Label>
            </p>
            <p>
                <asp:TextBox ID="txtVolareObjektId" runat="server" ToolTip="Eindeutige Objekt ID, zum Beispiel o:128758" Width="200px" OnTextChanged="txtVolareObjektId_TextChanged">o:128758</asp:TextBox>
            </p>

            <p>
                <asp:Button ID="btnErkenneOjekte" runat="server" OnClick="btnErkenneObjekte_Click" Text="Metadaten mittels KI erstellen" />
            </p>

            <p>
                <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <asp:Literal ID="ltContent" runat="server"></asp:Literal>
            </p>
            <p>
                <asp:Image ID="imgShowVolareImage" runat="server" Visible="False" CssClass="volareimage" />
            </p>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
