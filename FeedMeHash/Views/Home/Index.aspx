<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page - My ASP.NET MVC Application
</asp:Content>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: ViewBag.Message %></h1>
            </hgroup>            
        </div>
    </section>
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <form id="formHash" style="width : 90%">
        Please enter in the hashtag you wish to search for, without the '#' symbol <br />
        <input type="text" id="txtHash" style="width : 50%"/>
        <input type="button" id="btnSubmit" value="Search"/>
    </form>
    <div id="divTweets" hidden="hidden"></div>


    <script id="tweetTemplate" type="text/x-handlebars-template">
        {{#each tweets}} 
        <table id="tblTweets" style="border : 1px solid DarkTurquoise " width="95%">                 
            <tr>
                <td align=center class="hashCells" width="25%">
                    <img src="{{this.profile_image_url}}" style="max-width : 100px"/> 
                </td>
                <td align=left width = "50%" class="hashCells" style="color : {{this.profile_text_color}}">
                    <h2>{{this.name}}</h2>
                </td>
                <td class="hashCells">
                    <a href="{{this.url}}" target="_blank">@{{this.screen_name}}</a>
                </td>
            </tr>
            <tr>
                 <td colspan=3 class="hashCells">
                    <img src="{{this.profile_background_image_url}}" width = "100%" height = "100px"/> 
                </td>
            </tr>
            <tr>
                <td colspan=3 class="hashCells">
                    {{this.text}}
                </td>
            <tr>
            <tr>
                <td></td>
                <td class="hashCells">
                    Tweeted at: {{this.created_at}}
                </td>
                <td class="hashCells">
                    Location: {{this.location}}
                </td>       
        </table>   
        {{/each}}     
    </script>


    <script src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script src="../../Scripts/HashSearch.js"></script>
</asp:Content>
