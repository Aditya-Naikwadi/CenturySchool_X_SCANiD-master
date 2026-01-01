<%@ Page Title="" Language="C#" MasterPageFile="~/FeesModule/Master.Master" AutoEventWireup="true" CodeFile="ModuleMaster.aspx.cs" Inherits="CenturyRayonSchool.FeesModule.ModuleMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .div-academicyear {
            position: absolute;
            top: 12px;
            right: 14px;
        }

        .ftrow {
            font-size: small;
            text-align: center;
        }

        .ftrow1 {
            padding: 4px;
            color: black;
            font-weight: bolder;
            height: 50px;
            width: 250px;
            text-align: center;
        }


        .c-visible {
            display: none;
        }

        .uppercase {
            text-transform: uppercase;
        }

        .btn-dark {
            border-radius: 50px !important;
        }

        .text-left {
            text-align: left;
        }

        .text-center {
            text-align: center;
            font-size: 20px;
        }

        .col-id-no {
            left: 0 !important;
            position: sticky !important;
            background-color: lightgray;
        }

        .col-id {
            background-color: lightblue;
        }

        .fixed-header {
            z-index: 50 !important;
        }
    </style>
    <link href="../css/sumoselect.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="p-2">
        <div class="card card-sh">
            <div class="card-header card-mobile">
                <a href="Dashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>
                Module Master
                <div class="div-academicyear">
                    <asp:Label Text="Academic Year: " runat="server" />
                    <asp:Label Text="" runat="server" ID="lblacademicyear" />
                </div>
            </div>
            <div class="card-body margin-rows">
                <div class="row">
                    <div class="col-md-3">
                        <label for="cmbustype" class="form-label mb-1">User Name</label>
                        <asp:DropDownList ID="cmbusr" class="form-control select2" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:CustomValidator ID="usrnmeCustomvalid" runat="server" ErrorMessage="Select User Name" ControlToValidate="cmbusr" OnServerValidate="usrnmeCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                    <div class="col-md-6">
                        <label for="listboxModule" class="form-label mb-1">Module List</label><br />
                        <asp:ListBox ID="listboxModule" class="form-control" runat="server" SelectionMode="Multiple"></asp:ListBox>
                    </div>

                    <div class="col-md-3" style="padding-top: 16px;">
                        <button runat="server" id="Savedetais" class="btn btn-saveData mt-2" onserverclick="Savedetais_ServerClick" style="width: 100%; height: 60%;"><i class="fas fa-angle-double-down mr-2"></i>Save Data</button>
                    </div>

                </div>
                <div class="col-md-12">
                    <asp:GridView CssClass="table table-lg table-striped table-responsive mt-3 ftrow" AutoGenerateColumns="false" ID="gridHeaders" runat="server" Width="100%" ShowHeaderWhenEmpty="true">
                        <Columns>
                            <asp:BoundField DataField="userid" HeaderText=" User Id " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-center " ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                            <asp:BoundField DataField="UserName" HeaderText=" User Name " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-center " ItemStyle-Width="10%" HeaderStyle-Width="20%" />
                            <asp:BoundField DataField="modulename" HeaderText="Module Name" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-center " ItemStyle-Width="10%" HeaderStyle-Width="20%" />
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
    </section>
    <div class="modal fade" id="alertmessagemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="font-weight: bold;">Alert Message!!</h5>

                    <img src="../img/alertimage.png" style="height: 100px; width: auto;" />
                </div>
                <div class="modal-body">
                    <asp:Label Text="" runat="server" ID="lblalertmessage" Style="font-weight: normal;" />
                    <%--<label id="lblmessage" style="font-weight: normal;"></label>--%>
                </div>
                <div class="modal-footer">
                    <button type="button1" class="btn btn-danger" data-dismiss="modal">OK</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="infomessagemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="font-weight: bold;">Information Message.</h5>

                    <img src="../img/information.png" style="height: 100px; width: auto;" />
                </div>
                <div class="modal-body">
                    <asp:Label Text="" runat="server" ID="lblinfomsg" Style="font-weight: normal;" />
                    <%--<label id="lblinfomsg" style="font-weight: normal;"></label>--%>
                </div>
                <div class="modal-footer">
                    <button type="button" id="infobtn" class="btn btn-success" data-dismiss="modal">OK</button>
                </div>
            </div>
        </div>
    </div>
    
    <script src="../css/jquery.sumoselect.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function (e) {

            $(".select2").select2();
            $(<%=listboxModule.ClientID%>).SumoSelect();
        });

        function showInfoModal() {
            var myModal = new bootstrap.Modal(document.getElementById('infomessagemodal'))
            myModal.show()
        }

        function showAlertModal() {
            var myModal = new bootstrap.Modal(document.getElementById('alertmessagemodal'))
            myModal.show()
        }
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you really want to Delete this Record?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

       

    </script>
</asp:Content>
