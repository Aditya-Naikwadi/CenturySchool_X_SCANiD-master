<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login_1.aspx.cs" Inherits="CenturyRayonSchool.Login_1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
    <meta charset="UTF-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>

    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,600,700,700i&display=swap" rel="stylesheet"/>
    <link href="css/bootstrap.css" rel="stylesheet"/>
    <link href="css/fontawesome-all.css" rel="stylesheet"/>
    <link href="css/swiper.css" rel="stylesheet"/>
	<link href="css/magnific-popup.css" rel="stylesheet"/>

    <script src="js/jquery-3.5.1.js"></script>
	<script src="js/bootstrap.min.js"></script>

    <title>Admin Login</title>
    <style>
        #bg {
            position: fixed;
            top: -20%;
            left: -50%;
            width: 200%;
            height: 100%;
        }

            #bg img {
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                margin: auto;
                min-width: 50%;
                min-height: 50%;
                display: block;
            }

        header {
            display: block;
        }
    </style>

    <script>
        $(document).ready(function (e) {

            //showErrorModal();


        });

        function showErrorModal() {
            $("#errorMessageModal").modal("show");
        }
    </script>
</head>
<body>
    

   
    <div id="bg">
        <img src="img\header-background.jpg" alt="">
    </div>
    
    <header id="header" class="header">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12" style="padding-right: 0px; padding-left: 0px; text-align: center;">
                    <div class="text-container" style="background-color: rgb(92, 91, 87); color: orange; padding-top: 20px; padding-bottom: 10px;" style="width: 100%;">

                        <h1>Century Rayon High School, Shahad </h1>
                        <h3 class="font-2">Kalyan - Ahmednagar Highway, Century Rayon Colony, Shahad, Ulhasnagar, Maharashtra 421103</h3>

                    </div>
                </div>
            </div>
        </div>


        <div class="outer-container">

            <div style='width: 1500px; max-width: 100%; margin: auto'>
                <div id='slider-1'></div>
            </div>
        </div>
    </header>
    <section class="vh-100 gradient-custom">
        <form id="Login" runat="server">
            <asp:ScriptManager ID="SCPTMGR" runat="server" EnableCdn="true"></asp:ScriptManager>
            <div class="container  h-100">
                <div class="row d-flex justify-content-center h-100">
                    <div class="col-12 col-md-8 col-lg-6 col-xl-5">
                        <div class="card bg-dark text-white" style="border-radius: 1rem; margin-top: 20px;">
                            <div class="card-body p-5 text-center">
                                <div >  <asp:Label ID="Label1" runat="server" style="color:orangered;font-weight:bold;"></asp:Label> </div>
                                <div class="mb-md-5 mt-md-4 pb-5">

                                    <h2 class="fw-bold mb-2" style="color: white">Admin Login</h2>
                                    <p class="text-white-50 mb-5">Please Enter Your Login and Password!</p>

                                    <div class="form-outline form-white mb-4">
                                        <asp:TextBox ID="Uname" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                                        <label class="form-label" for="typeEmailX">Enter User Name</label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Uname" ErrorMessage="Please Enter Your Username"
                                            ForeColor="Red"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-outline form-white mb-4">
                                        <asp:TextBox ID="Upass" runat="server" CssClass="form-control form-control-lg" TextMode="Password"></asp:TextBox>

                                        <label class="form-label" for="typePasswordX">Enter Password</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Upass" ErrorMessage="Please Enter Your Username"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:Button ID="btn1_Login" runat="server" Text="Login" CssClass="btn btn-outline-light btn-lg px-5" onclick="btn1_Login_Click"/>
                                  

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </section>
    <div class="modal" id="errorMessageModal">
              <div class="modal-dialog">
                <div class="modal-content">

                  <!-- Modal Header -->
                  <div class="modal-header">
                    <h4 class="modal-title">Error Message</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                  </div>

                  <!-- Modal body -->
                  <div class="modal-body">
                     <asp:Label ID="errorMessage" runat="server" style="font-weight:bold;"></asp:Label>
                  </div>

                  <!-- Modal footer -->
                  <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                  </div>

                </div>
              </div>
    </div>
    
        
</body>
</html>
