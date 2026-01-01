<%@ Page Title="Search Form" Language="C#" MasterPageFile="~/AdmissionModule/MasterPage.Master" AutoEventWireup="true" CodeFile="Admissionlabel.aspx.cs" Inherits="CenturyRayonSchool.AdmissionModule.Admissionlabel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .custom-label {
        display: block;
        text-align: center;
        color: red;
        font-size: 42px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <br />
    <asp:Label runat="server" ID="lblended" Text="Admission is Closed... Starting Soon..." CssClass="custom-label" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblstarted" Text="Admission not yet started... Starting Soon..." CssClass="custom-label" Visible="false"></asp:Label>

</asp:Content>
