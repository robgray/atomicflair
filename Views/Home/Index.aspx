<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AtomicUserViewModel>" %>
<%@ Import Namespace="atomicflair.Controllers"%>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="color: White">
    <h2>Atomic flair</h2>
    <div>
        This service generates a small badge indicating some stats of the atomic user, including:
        <ul>
            <li>Name</li>
            <li>Avatar</li>
            <li>Any special ranks</li>
            <li>Rank based on postcount</li>
        </ul>
        The idea is the atomican can display this on their site or blog, much like stack overflows flairs.
    </div>
    <% using (Html.BeginForm("Id", "User", FormMethod.Post))
       { %>
    <p>
    Enter Atomic User Id to generate your atomic flair <%= Html.TextBox("userId") %> 
    <input type="submit" value="Generate" />
    </p>
    <% } %>
    </div>
</asp:Content>
