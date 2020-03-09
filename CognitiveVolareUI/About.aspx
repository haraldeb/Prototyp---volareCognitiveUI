<%@ Page Title="Prototyp" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="CognitiveVolareUI.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p>Mit Hilfe dieses Prototyps lassen sich alle Bilder aus dem Vorarlberger Landesrepositorium mittels künstlicher Intelligenz auf folgende Kriterien untersuchen.</p>
    <h3>Identifizierung von landeskundlich relevanten Personen</h3>
    <p>Für diesen Prototyp wurden 47 landeskundlich relevante Personen gewählt, die auf ausreichend Lichtbildwerken im Bestand der Vorarlberger Landesbibliothek abgebildet sind. Anhand dieser Lichtbildwerke wurde ein Modell zur Gesichtserkennung trainiert. </p>
    <p>
        <a class="btn btn-default" href="Personen">Personenerkennung &raquo;</a>
        <a class="btn btn-default" href="TrainingsdatenPersonen">Trainingsdaten &raquo;</a>
    </p>
    <h3>Erkennung von Objekten anhand von Maschinellem Sehen</h3>
    <p>Der Azure-Dienst für Maschinelles Sehen bietet erweiterte Algorithmen, die Bilder verarbei-ten und entsprechend Informationen zurückgeben. Das Standardmodell von Mircosoft wird dabei mit vielen umfangreichen Datensätzen trainiert und laufend weiterentwickelt.</p>
        <p>
        <a class="btn btn-default" href="Objekte">Objekterkennung &raquo;</a>
    </p>
    <h3>Erkennung und Beschreibung markanter landeskundliche Objekte</h3>
    <p>Für diesen Prototyp wird das Modell mit sechs markanten landeskundlichen Objekte trainiert. Mircosoft empfiehlt, das Modell mit etwa 50 Bildern pro Objekt zu trainieren. Im historischen Kontext stellt dies ein großes Problem dar, da nur in den wenigen Fällen so viele Aufnahmen in ausreichender Qualität überlieft wurden. Zudem werden neben den Trainingsdaten auch noch Daten zur Validierung benötigt. </p>
    <p>
        <a class="btn btn-default" href="LandeskundlicheElemente">Erkennung landeskundlicher Elemente &raquo;</a>
        <a class="btn btn-default" href="TrainingsdatenLandeskundlicheElemente">Trainingsdaten &raquo;</a>
    </p>
    <h3>Erstellung maschinenlesbarer Metadaten</h3>
    <p>Für die weitere Verarbeitung erkannten inhaltsbasierten Merkmale, werden die Daten in ein standardisiertes und maschinenlesbares Format gebracht. Für diesen Prototyp wird die Ausgabe im MARC21-Format umgesetzt und als MARC-XML ausgegeben. </p>
        <p>
        <a class="btn btn-default" href="Katalogisat">Metadatenerstellung &raquo;</a>
    </p>
</asp:Content>
