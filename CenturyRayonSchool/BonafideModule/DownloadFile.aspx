<%@ Page Title="" Language="C#" MasterPageFile="~/BonafideModule/BonafideMaster.Master" AutoEventWireup="true" CodeFile="DownloadFile.aspx.cs" Inherits="CenturyRayonSchool.BonafideModule.DownloadFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <iframe src="<%=fileurl%>" width="800" height="1000">

    </iframe>
</asp:Content>
