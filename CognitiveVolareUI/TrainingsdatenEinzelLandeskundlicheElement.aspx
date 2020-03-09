<%@ Page Title="Trainingsdaten Personen" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrainingsdatenEinzelLandeskundlicheElement.aspx.cs" Inherits="CognitiveVolareUI.TrainingsdatenEinzelLandeskundlicheElement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>
        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label></h3>
        <asp:Label ID="lblGND" runat="server" Text="Name"></asp:Label>
    <p>
        <asp:Literal ID="ltContent" runat="server"></asp:Literal>
    </p>
</asp:Content>
