﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Mediatek.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Movies
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Movies</h2>
    <table>
        <thead>
            <tr>
                <th>Cover</th>
                <th>Title</th>
                <th>Released</th>
                <th>Contributors</th>
            </tr>
        </thead>
        <tbody>

        <%
            var movies = Model as IEnumerable<Mediatek.Entities.Movie>;
            if (movies != null)
            {
                foreach (var m in movies)
                {
        %>
        
            <tr>
                <td><img src="<%=m.Picture.GetPath(true)%>" alt="<%=m.Picture.Name%>" title="<%=m.Picture.Name%>" /></td>
                <td><%=Html.Encode(m.Title)%></td>
                <td><%=Html.CriteriaLink(m.Year.ToString(), new { year = m.Year })%></td>
                <td><%=Html.BulletedList(
                        m.Contributions
                            .OrderBy(c => c.Role.Name)
                            .ThenBy(c => c.Person.DisplayName)
                            .Select(c => MvcHtmlString.Create(string.Format(
                                "{0} ({1})",
                                Html.CriteriaLink(
                                    c.Person.DisplayName,
                                    new { contributor = c.PersonId }),
                                c.Role.Name))))%></td>
            </tr>
            <%
                }
            }
            %>

        </tbody>
    </table>
    
</asp:Content>
