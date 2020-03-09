<%@ Page Title="Trainingsdaten Personen" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrainingsdatenPersonen.aspx.cs" Inherits="CognitiveVolareUI.TrainingsdatenPersonen" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p>
    Über den Menüpunkt &quot;<a href="Personen">Personen</a>&quot; können alle in volare verfügbaren Lichtbilder nach Personen untersucht werden. 
    <br />Das Modell wurde folgenden Personen trainiert:<br />
    </p>
    <hr />
    <p>
        <asp:Literal ID="ltContent" runat="server"></asp:Literal>
    </p>
</asp:Content>
