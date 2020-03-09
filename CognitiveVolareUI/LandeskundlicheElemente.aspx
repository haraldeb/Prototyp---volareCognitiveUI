<%@ Page Title="Erkennung von landeskundlichen Elementen" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LandeskundlicheElemente.aspx.cs" Inherits="CognitiveVolareUI.LandeskundlicheElemente" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <asp:UpdateProgress runat="server" ID="PageUpdateProgress">
        <ProgressTemplate>
            <div class="overlay">
                <div class="centered">
                    <img src="Images/giphy.gif" />
                    <p>Ich suche nach landeskundl. Elementen.</p>
                </div>
            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="Panel">
        <ContentTemplate>
            <p>
                <asp:Label ID="lblVolareObjektId" runat="server" Text="volare-Objekt-ID: (zb. o:153949, o:242800, o:27700, o:28273)"></asp:Label>
            </p>
            <p>
                <asp:TextBox ID="txtVolareObjektId" runat="server" ToolTip="Eindeutige Objekt ID, zum Beispiel o:153949" Width="200px" OnTextChanged="txtVolareObjektId_TextChanged">o:28273</asp:TextBox>
            </p>
            <p>

                <asp:Button ID="btnErkenneOjekte" runat="server" OnClick="btnErkenneObjekte_Click" Text="Landeskundliche Elemente erkennen" />

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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
