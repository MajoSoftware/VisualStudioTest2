<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutocompleteControlPage.aspx.cs" Inherits="WebApplication1.AutocompleteControlPage" %>
<%@ Register TagPrefix="myControls" Namespace="WebApplication1.CustomControls" Assembly="WebApplication1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" />

    <!-- Styles for menu-item and textbox -->
    <style type="text/css" >
        .ui-menu .ui-menu-item { margin: 0; padding: 0; zoom: 1; width: 500px; font-size : 8pt; color : blue; }
        .ui-autocomplete-input { font-size : 8pt; color : red; }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <myControls:AutoComplete2 
            ID="MyAutoComplete2" 
            runat="server" 
            JavascriptFile="~/Scripts/Controls/autocompletecontrolv2.js" 
            DataUrl="~/Handlers/AutoCompleteHandler.ashx" 
            SearchParameterName="searchTerm"
            ContextKey="myContext"
        />

        <asp:Button ID="Button1" runat="server" Text="Button" />

    </div>
    </form>
</body>
</html>
