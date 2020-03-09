<%@ Page Title="Trainingsdaten für landeskundliche Elemente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrainingsdatenLandeskundlicheElemente.aspx.cs" Inherits="CognitiveVolareUI.TrainingsdatenLandeskundlicheElemente" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p>
    Über den Menüpunkt &quot;<a href="LandeskundlicheElemente">Landeskundliche Elemente </a>&quot; können alle in volare verfügbaren Lichtbilder nach trainierten landeskunglichen Elementen untersucht werden. 
    <br />Das Modell wurde folgenden Elemente trainiert:<br />
    </p>
    <hr />
    <p>
        <asp:Literal ID="ltContent" runat="server"></asp:Literal>
    </p>
</asp:Content>
