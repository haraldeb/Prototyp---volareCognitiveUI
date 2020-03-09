<%@ Page Title="volare Cognitive UI - Startseite" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CognitiveVolareUI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>volare Cognitive UI</h1>
        <p class="lead">Prototyp zur maschinellen Erkennung und Erschließung von historischen Bildinhalten aus dem Bestand der Vorarlberger Landesbibliothek.</p>
        <p><a href="About" class="btn btn-primary btn-lg">Mehr erfahren &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Erkennung von Vorarlberger Personen</h2>
            <p>
                Dieses Modell wurde mit 47 Vorarlberger Personen aus Politik und Gesellschaft trainiert.</p>
            <p>
                <a class="btn btn-default" href="TrainingsdatenPersonen">Trainingsdaten ansehen &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Erkennung von Objekten und Texten</h2>
            <p>
                Objekte, Gegenstände und Texte (OCR) auf historischem Bildmaterial anhand des Standardmodells der Microsoft Cognitive Services erkennen.</p>
            <p>
                <a class="btn btn-default" href="Objekte">Mehr erfahren &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Erkennung von landeskundlichen Objekten</h2>
            <p>
                Dieses Modell wurde mit landeskundlichen Objekten wie bespielsweise Bauten und markanten Landschaftselementen trainiert.
            </p>
            <p>
                <a class="btn btn-default" href="TrainingsdatenLandeskundlicheElemente">Trainingsdaten ansehen &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
