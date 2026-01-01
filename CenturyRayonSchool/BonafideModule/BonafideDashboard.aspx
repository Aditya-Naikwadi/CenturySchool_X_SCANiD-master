<%@ Page Title="" Language="C#" MasterPageFile="~/BonafideModule/BonafideMaster.Master" AutoEventWireup="true" CodeFile="BonafideDashboard.aspx.cs" Inherits="CenturyRayonSchool.BonafideModule.BonafideDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .font-color {
            color: navy !important;
        }

        .uppercase {
            text-transform: uppercase;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div class="row">
            <div class="col-sm-10">
                <h3 class="page-title fs-16">Welcome, &nbsp
                    <asp:Label runat="server" Text="Insiya Kanchwala" ID="lbldisplayusername" CssClass="uppercase"></asp:Label></h3>

            </div>
            <div class="col-sm-2">
                <asp:Label Text="Academic Year :" runat="server" />
                <h3 class="page-title fs-16">
                    <asp:Label runat="server" ID="lblAcademicyear"></asp:Label>

                </h3>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">

                <ul class="breadcrumb">
                    <li class="breadcrumb-item active">Dashboard</li>
                </ul>
            </div>
        </div>
    </div>
    <section class="content mb-1">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-4">
                    <a href="GenrateBonafide.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-list"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Bonafide Generation</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>

                <div class="col-md-4">
                    <a href="BulkBonafide.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-server"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Bulk Bonafide Generation</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>

                <div class="col-md-4">
                    <a href="PrintBonafide.aspx">
                        <div class="card dash-widget border-card">
                            <div class="card-body">
                                <span class="dash-widget-icon"><i class="fas fa-rupee-sign"></i></span>
                                <div class="div-header">
                                    <span class="span-header font-color">Print Bonafide</span>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
    </section>
    <script>
        $(document).ready(function () {
            $("#menu_dash").addClass("active");
        });
    </script>
</asp:Content>
