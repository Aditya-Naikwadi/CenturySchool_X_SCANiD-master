<%@ Page Title="" Language="C#" MasterPageFile="~/FeesModule/ModuleSelection.Master" AutoEventWireup="true" CodeBehind="SelectModule.aspx.cs" Inherits="CenturyRayonSchool.FeesModule.SelectModule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .font-color{
            color:navy !important;
        }

        .uppercase{
            text-transform:uppercase;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="page-header">
        <div class="row">
            <div class="col-sm-10">
                <h3 class="page-title fs-16">Welcome, &nbsp <asp:Label runat="server" Text="Insiya Kanchwala" ID="lbldisplayusername" CssClass="uppercase"></asp:Label></h3>
               
            </div>
            <div class="col-sm-2">
                <asp:Label Text="Academic Year :" runat="server" />
                 <h3 class="page-title fs-16" >
                     <asp:Label runat="server" ID="lblAcademicyear" ></asp:Label>

                 </h3>
            </div>
        </div>
        <div class="row">
             <div class="col-sm-12">
                
                <ul class="breadcrumb">
                    <li class="breadcrumb-item active">Module Selection</li>
                </ul>
            </div>
        </div>
    </div>
     <section class="content mb-1">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md 12">
                   <Button runat="server" ID="feemodule" class="btn btn-primary btn-lg btn-block" onserverclick="feemodule_ServerClick">Go To Fees Module</Button>
                </div>
                <div class="col-md 12">
                   <Button runat="server" ID="resultmodle" class="btn btn-primary btn-lg btn-block" onserverclick="resultmodle_ServerClick">Go To Marksheet Module</Button>
                </div>
                </div>
            </div>
         </section>
</asp:Content>
