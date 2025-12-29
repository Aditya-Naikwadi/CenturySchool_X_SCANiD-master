<%@ Page Title="" Language="C#" MasterPageFile="~/ParentsModule/ParentModule.Master" AutoEventWireup="true" CodeBehind="ParentsDashboard.aspx.cs" Inherits="CenturyRayonSchool.ParentsModule.ParentsDashboard" %>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
        <style>
        .hyper:hover {
                color: navy !important;
        }   
    </style>

     <div style="display: none;">
            <asp:Label Text="-1" runat="server" ID="lbl_grno" />
            <asp:Label Text="-1" runat="server" ID="lbl_std" />
            <asp:Label Text="-1" runat="server" ID="lbl_year" />
            <asp:Label Text="-1" runat="server" ID="lbl_username" />
            <%--this is important--%>
        </div>
    <div class="content-wrapper">

        <section class="content">
            <div class="container-fluid">
                <!-- Breadcubs Area Start Here -->
                <div class="breadcrumbs-area">
                    <asp:Label CssClass="h4" runat="server">Welcome ,</asp:Label>
                    <asp:Label CssClass="h4" runat="server" ID="fullname"></asp:Label>
                </div>
                <!-- Breadcubs Area End Here -->
                <!-- Dashboard summery Start Here -->

                <!-- Dashboard summery End Here -->
                <!-- Dashboard Content Start Here -->
                <div class="row gutters-20">
                    <div class="col-12 col-xl-8 col-6-xxxl">
                        <div class="card dashboard-card-one pd-b-20">
                            <div class="card-body">
                                <div class="heading-layout1">
                                    <div class="item-title">
                                        <h3> <i class="fas fa-id-badge" style="color:blue;font-size:20px"></i>&nbsp &nbsp Student Profile</h3>
                                    </div>
                                </div>

                                <div class="row" style="padding:6px">
                                    <div class="col-md-4 col-sm-5">
                                        <asp:Image  id="picturepath" runat="server" width="100" CssClass="rounded-circle"/>
                                    </div>
                                    <div class="col-md-8 col-sm-5">
                                        <label class="login__field">Student Name</label>
                                        <asp:TextBox runat="server" type="text" class="login__input" ID="StudentName" ReadOnly></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row" style="padding:6px">
                                    <div class="col-md-3 col-sm-5" >
                                        <label class="login__field">RollNo : </label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="RN" ReadOnly></asp:TextBox>

                                    </div>
                                    <div class="col-md-3 col-sm-5" >
                                        <label class="login__field">GRNO : </label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="GR" ReadOnly></asp:TextBox>

                                    </div>
                                    <div class="col-md-3 col-sm-5" >
                                        <label class="login__field">CID : </label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="txtcid" ReadOnly></asp:TextBox>

                                    </div>
                                    <div class="col-md-3 col-sm-5" >
                                        <label class="login__field">STD : </label>
                                        <asp:TextBox runat="server" type="text" class="login__input" ID="standared" ReadOnly></asp:TextBox>

                                    </div>
                                    <div class="col-md-3 col-sm-5" >
                                        <label class="login__field">DIV : </label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="division" ReadOnly></asp:TextBox>

                                    </div>
                                </div>
                                <div class="row" style="padding:6px" >
                                    <div class="col-md-4 col-sm-5" >
                                        <label class="login__field">Father Name :</label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="father" ReadOnly></asp:TextBox>

                                    </div>
                                    <div class="col-md-4 col-sm-5" >
                                        <label class="login__field">Mother Name :</label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="mother" ReadOnly></asp:TextBox>

                                    </div>
                                    <div class="col-md-4 col-sm-5" >
                                        <label class="login__field">Contact No.1 :</label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="mobileno" ReadOnly></asp:TextBox>

                                    </div>
                                </div>

                                <div class="row" style="padding:6px" >
                                    <div class="col-md-4 col-sm-5" >
                                        <label class="login__field">Contact No. 2 :</label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="mobileno2" ReadOnly></asp:TextBox>
                                        </div>
                                      <div class="col-md-4 col-sm-5" >
                                          <label class="login__field">DOB :</label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="Dateofbirth" ReadOnly></asp:TextBox>
                                        </div>
                                      <div class="col-md-4 col-sm-5" >
                                          <label class="login__field">DOA :</label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="dateofadmission" ReadOnly></asp:TextBox>
                                        </div>
                                </div>
                                <div class="row" style="padding:6px" >
                                       <div class="col-md-12 col-sm-5" >
                                             <label class="login__field">Address :</label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="address" ReadOnly></asp:TextBox>
                                           </div>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="col-12 col-xl-8 col-6-xxxl">
                        <div class="card dashboard-card-three pd-b-20">
                            <div class="card-body">
                                <div class="heading-layout1">
                                    <div class="item-title">
                                      <h3><i class="fa-solid fa-indian-rupee-sign" style="color:blue;font-size:20px"></i>&nbsp &nbsp Fee Details</h3>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6" style="padding:6px">
                                          <label class="login__field">Total Fees :</label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="totlfee" ReadOnly></asp:TextBox>
                                    </div>
                                     <div class="col-md-6 col-sm-6" style="padding:6px">
                                          <label class="login__field">Ammount Paid till Now :</label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="amntpaid" ReadOnly></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 col-sm-6" style="padding:6px">
                                          <label class="login__field">Freeship Ammount :</label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="freeship" ReadOnly>0</asp:TextBox>
                                    </div>
                                    <div class="col-md-6 col-sm-6" style="padding:6px">
                                         <label class="login__field">Balance Fees :</label>
                                        <asp:TextBox runat="server" type="text" class="login__input " ID="balanceamnt" ReadOnly></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                     <div class="col-md-12 col-sm-6" style="padding:6px">
                                          <asp:TextBox runat="server" type="text" class="login__input " ID="status" Font-Size="XX-Large" ForeColor="brown" ReadOnly></asp:TextBox>
                                         </div>
                                </div>
                                <div class="row" >
                                    <div  class="col-md-12 col-sm-6" style="padding:6px;text-align: center;">
                                        <asp:Button runat="server" id="printreceipt" class="btn btn-success btn-hover-bluedark btn-lg" OnClick="printreceipt_Click" Text="RePrint Receipt"/>
                                        <%--<asp:HyperLink NavigateUrl='<%#getDownloadUrl(lbl_year.Text,GR.Text,standared.Text)%>' ID="hyperlink" runat="server" Text="Receipt" CssClass="btn btn-success btn-hover-bluedark btn-lg" Target="_blank" />--%>
                                    </div>
                                    <%-- <div  class="col-md-12 col-sm-6" style="padding:6px;text-align: center;">
                                        <asp:Button runat="server" id="paynow" class="btn btn-primary btn-hover-bluedark btn-lg" OnClick="paynow_Click" Text="Pay Now"/>
                                    </div>--%>
                                    <div  class="col-md-12 col-sm-6" style="padding:6px;text-align: center;">
                                        <asp:Button runat="server" id="paynow_online" class="btn btn-danger btn-hover-bluedark btn-lg" OnClick="paynow_online_Click" Text="Pay Now Online"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>


    </div>
      <div class="card-group" >
                        <div class="card" >
                            <div class="card-body" style="margin:0px;padding:0px; text-align: center">
                                <div class="nicdark_textevidence center event-bday">
                                    <div class="nicdark_textevidence nicdark_width_percentage30 nicdark_bg_violet nicdark_shadow nicdark_radius_left info">
                                        <div class="nicdark_textevidence" style="background-color: #ac7ab5;color:black;">
                                            <div class="nicdark_margin30" >
                                                <h2 class="white subtitle"  style="margin:0px;padding:0px;color:white">TODAY'S BIRTHDAYS</h2>
                                            </div>
                                            <i class="nicdark_zoom icon-music- nicdark_iconbg left extrabig blue nicdark_displaynone_ipadland nicdark_displaynone_ipadpotr"></i>
                                        </div>

                                        <div class="nicdark_archive1 nicdark_bg_orange nicdark_radius nicdark_shadow" style="height: 250px; padding-left: 25px; background-color: #df764e; ">

                                            <marquee direction="up" scrollamount="3" onmouseover="this.stop();" onmouseout="this.start();" loop="true" style="height: 250px; color:white">
                                                <%--<h2 style="font-family: sans-serif; color: blue;">Student's Birthdays</h2>--%>
                                                <ul style="font-family: 'Mongolian Baiti'; color: Blue; text-align: left; margin: 10px;">
                                                    <%=studentbirthday%>
                                                </ul>

                                                <%--<h2 style="font-family: sans-serif; color: blue;">Staff's Birthdays</h2>--%>
                                                <%--<ul style="font-family: 'Mongolian Baiti'; color: red; text-align: left; margin: 10px;">
                                                <%=staffbirthday%>
                                            </ul>--%>
                                            </marquee>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                   
                    <div class="card">
                        <div class="card-body" style="margin:0px;padding:0px; text-align: center">
                            <div class="nicdark_textevidence nicdark_width_percentage30 nicdark_bg_yellow nicdark_shadow">
                                <div class="nicdark_textevidence" style="background-color:#e0b84e">
                                    <div class="nicdark_margin30 m-43">
                                        <h2 class="white subtitle" style="margin:0px;padding:0px"><a href="../News.aspx" style="color:white">NOTICES</a></h2>
                                    </div>
                                    <i class="nicdark_zoom icon-pencil-2 nicdark_iconbg left extrabig yellow nicdark_displaynone_ipadland nicdark_displaynone_ipadpotr"></i>
                                </div>

                                <div class="nicdark_archive1 nicdark_bg_violet nicdark_radius nicdark_shadow" style="height: 250px;">
                                    <marquee direction="up" scrollamount="3" onmouseover="this.stop();" onmouseout="this.start();" loop="true" style="height: 250px; background-color: #ac7ab5">


                                        <asp:ListView ID="ListViewNews" runat="server">
                                            <LayoutTemplate>
                                                <div style="width: 100%;">
                                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>

                                                <div style="text-transform: capitalize; text-align: left; margin: 10px; font-size: 17px; font-family: sans-serif;">
                                                    <li><a class="hyper" href="NewsDescripition.aspx?id=<%# Eval("id")%>" id="newslist" target="_blank" style="color: white">
                                                        <%# Eval("TopicName")%></a></li>
                                                </div>


                                            </ItemTemplate>

                                            <EmptyDataTemplate>
                                                No records found
                                            </EmptyDataTemplate>
                                        </asp:ListView>

                                    </marquee>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="card">

                        <div class="card-body" style="margin:0px;padding:0px;text-align: center">
                            <div class="nicdark_textevidence nicdark_width_percentage30 nicdark_bg_orange nicdark_shadow">
                                <div class="nicdark_textevidence"  style="background-color: #df764e">
                                    <div class="nicdark_margin30">
                                        <h2 class="white subtitle" style="margin:0px;padding:0px"><a style="color:white" href="../Event.aspx">UPCOMING EVENTS</a></h2>
                                    </div>
                                    <i class="nicdark_zoom icon-music- nicdark_iconbg left extrabig orange nicdark_displaynone_ipadland nicdark_displaynone_ipadpotr"></i>
                                </div>
                                <div class="nicdark_archive1 nicdark_bg_green nicdark_radius nicdark_shadow" style="height: 250px; background-color: #6ab78a">
                                    <marquee direction="up" scrollamount="3" onmouseover="this.stop();" onmouseout="this.start();" loop="true" style="height: 250px;">


                                        <asp:ListView ID="ListViewEvent" runat="server"  >
                                            <LayoutTemplate>
                                                <div style="width: 100%;">
                                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>



                                                <div style="color: white !important; /*font-weight: bold; */text-transform: capitalize; /*font-size: 30px; */ text-align: left; margin: 10px; font-family: sans-serif; font-size: 17px">
                                                    <li><a class="hyper" href="../EventDescripition.aspx?id=<%#Eval("id")%>" id="A1" style="color: white">
                                                        <%# Eval("eventName")%></a></li>
                                                </div>


                                            </ItemTemplate>

                                            <EmptyDataTemplate>
                                                No records found
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </marquee>
                                </div>
                            </div>

                        </div>


                         </div>
                        </div>
    <div class="modal fade" id="alertmessagemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="font-weight: bold;">Alert Message!!</h5>
                    
                    <img src="../img/alertimage.png" style="height:100px;width:auto;" />
                </div>
                <div class="modal-body">
                    <asp:Label Text="" runat="server" ID="lblalertmessage" style="font-weight: normal;" />
                    <%--<label id="lblmessage" style="font-weight: normal;"></label>--%>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">OK</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="infomessagemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="font-weight: bold;">Information Message.</h5>
                  
                    <img src="../img/information.png" style="height:100px;width:auto;" />
                </div>
                <div class="modal-body">
                    <asp:Label Text="" runat="server" ID="lblinfomsg" style="font-weight: normal;" />
                    <%--<label id="lblinfomsg" style="font-weight: normal;"></label>--%>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" data-dismiss="modal">OK</button>
                </div>
            </div>
        </div>
    </div>
    <script type = "text/javascript">
        function showInfoModal() {
            var myModal = new bootstrap.Modal(document.getElementById('infomessagemodal'))
            myModal.show()
        }

        function showAlertModal() {
            var myModal = new bootstrap.Modal(document.getElementById('alertmessagemodal'))
            myModal.show()
        }
 function SetTarget() {
     document.forms[0].target = "_blank";
 }
    </script>
</asp:Content>
