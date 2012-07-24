<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AtomicUserViewModel>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">      
    <div class="pp-name pp-name2">
        <div class="image">
            <% if (!string.IsNullOrEmpty(Model.ImageUrl)) {%>
            <img src='<%=Model.ImageUrl%>' width='48' height='48' alt='avatar' />
            <% } %>
        </div>			
        <div class="text">
            <h3><%= Model.Name %></h3>
			<b><span style="color:<%= Model.SpecialRankColor %>"><%= Model.SpecialRank %></span></b>							
			<div><%= Model.Rank %></div>
        </div>			
    </div>    
</asp:Content>
