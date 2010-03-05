<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

    <%
        var moviesLinkLabel = string.Format("{0} movies", ViewData["MovieCount"]);
     %>

    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <p>
        There are <%= Html.ActionLink(moviesLinkLabel, "Index", "Movie") %> in the database
    </p>
</asp:Content>
