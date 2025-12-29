<%@ Page Title="" Language="C#" MasterPageFile="~/LCModule/LeavingMaster.Master" AutoEventWireup="true" CodeBehind="DownloadFile.aspx.cs" Inherits="CenturyRayonSchool.LCModule.DownloadFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <iframe src="<%=fileurl%>" width="800" height="1000">

    </iframe>
</asp:Content>
