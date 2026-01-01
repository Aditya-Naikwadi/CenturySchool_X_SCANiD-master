<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeFile="Photogallery.aspx.cs" Inherits="CenturyRayonSchool.Photogallery1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .div1-style {
            background-color: #cccccc00; 
            font-family: Arial, Helvetica, sans-serif; 
            font-size: medium; 
            font-weight: normal; 
            font-style: normal;
            /*margin-left:100px;*/
            margin-top:50px;
            /*position:fixed;*/

        }

        .c-visible {
            display:none;
        }
        

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid dashboard-content" >
                   
                    <div class="row mb-2">
                        <div class="col-md-12">
                             <asp:Label ID="lblformtitle" Text="Add Images to Photo Gallery Section" runat="server" CssClass="font-weight-bolder " style="color:navy;font-size:xx-large;" />
                        </div>
                           
                    </div>

                       <div class="row">
                           
                           <div class="col-md-6">
                                <asp:Label ID="topic" runat="server" Text="Gallery Name" CssClass="font-weight-bold text-primary"></asp:Label>

                                <asp:TextBox ID="txtgalleryname" runat="server" class="form-control"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="required_gallery" runat="server" ErrorMessage="Kindly Enter Gallery Name" ControlToValidate="txtgalleryname" ForeColor="Red"></asp:RequiredFieldValidator>
                           </div>

                            <div class="col-md-6">
                                <asp:ScriptManager ID="SCPTMGR" runat="server" EnableCdn="true"></asp:ScriptManager>
                                  <label Class="font-weight-bold text-primary"> Photo Selection</label> <br />                                	
                               <%-- <input type="file" id="upld" name="upload" accept="image/*" onchange="loadFile(event)" multiple="multiple"  runat="server" />--%>
                                   <asp:UpdatePanel ID="UpdimageUpload" runat="server">  
                                        <ContentTemplate>  
                                            <asp:FileUpload ID="FileuploadImage" multiple="multiple" runat="server"  accept="image/*" onchange="loadFile(event)" />  
                                            
                                        </ContentTemplate>  
                                        <Triggers>  
                                            <asp:PostBackTrigger ControlID="btnSave"/>  
                                        </Triggers>  
                                    </asp:UpdatePanel>
                                <asp:Label id="err_label_fileimage" Text="Kindly Select Multiple Images" runat="server" ForeColor="Red" Visible="false"/>
                             </div>
                           <div class="col-md-6">
                                <asp:Label ID="description" runat="server" Text="Gallery Description" CssClass="font-weight-bold text-primary"></asp:Label>

                                <asp:TextBox ID="txtgalldescp" runat="server" class="form-control"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="required_gallerydescp" runat="server" ErrorMessage="Kindly Enter Gallery Description" ControlToValidate="txtgalldescp" ForeColor="Red"></asp:RequiredFieldValidator>
                           </div>
                           <div class="col-md-12">
                               <asp:Button ID="btnSave" class="btn btn-success" Text="Upload & Save" runat="server" OnClick="btnSave_Click" OnClientClick="loadGif(this.id)" UseSubmitBehavior="false"/>  
                                            <asp:Button ID="btnreset" class="btn btn-primary" Text="Reset" runat="server" OnClick="btnreset_Click" CausesValidation="False" /> 
                               </div>
                           <div class="col-md-12">
                                <asp:Label id="err_message" Text="" runat="server" ForeColor="Red" Visible="false"/>
                           </div>
                          
                       </div>

                      <div class="row mt-5 c-visible" id="progresdiv">
                          <div class="col-md-12">
                              <div style="background-color:navy;width:100%;text-align:center;color:white;">
                                  <img src="img/giphy.gif" alt="Alternate Text" style="width:50px;height:auto;" />
                                  <asp:Label ID="Label1" Text="Uploading Images..." runat="server" style="font-size:large;" />
                              </div>
                              
                          </div>
                      </div>
                        <div class="row mt-5 c-visible" id="loadingdiv">
                          <div class="col-md-12">
                              <div style="background-color:red;width:100%;text-align:center;color:white;">
                                  <img src="img/giphy.gif" alt="Alternate Text" style="width:50px;height:auto;" />
                                  <asp:Label ID="Label2" Text="Loading Images..." runat="server" style="font-size:large;" />
                              </div>
                              
                          </div>
                      </div>
                      
                   <div class="row mt-5">
                       <div class="col-md-12">
                           <asp:Label ID="lbluploadmessage" Text="" runat="server" style="font-size:large;color:blue;" />
                       </div>
                           <div class="col-md-12">
                                
                               <table class="table c-visible" id="table">
                                      <thead class="thead-dark">
                                        <tr>
                                          
                                          <th scope="col">Image</th>
                                          
                                        
                                        </tr>
                                      </thead>
                                    <tbody id="tb">
                                        <tr style="display:flex;flex-wrap:wrap;">

                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                       
                   </div>


                       
                </div>
        
            

        
           
        <script>

            var totalimages = 0;
            var imagesloaded = 0;
            $(document).ready(function () {

                $("#menu_photogallery").addClass("show");
                $("#item_photogallery").addClass("active");
                $("#menu_add_photogallery").addClass("active");
                $("#menu_add_photogallery").css("background-color", "white");
                $("#menu_add_photogallery").css("color", "black");

            });


            function getBase64(file, onLoadCallback) {
                return new Promise(function (resolve, reject) {
                    var reader = new FileReader();
                    reader.onload = function () { resolve(reader.result); };
                    reader.onerror = reject;
                    reader.readAsDataURL(file);
                });
            }


            var loadFile = function (event) {
                totalimages = 0;
                imagesloaded = 0;

                totalimages = event.target.files.length;

                $("#ContentPlaceHolder1_Label2").text("Loading Images Please Wait. Total Images " + totalimages);
                $("#loadingdiv").removeClass("c-visible");
              

                for (var i = 0; i < event.target.files.length; i++) {

                    $("#table").removeClass("c-visible");

                    var imgurl = URL.createObjectURL(event.target.files[i]);
                    console.log(event.target.files[i]);

                    var trow = "", gbase = "";

                    var promise = getBase64(event.target.files[i]);
                    promise.then(function (result) {
                        //console.log(result);
                        gbase = result;
                        trow = "<td><img src=" + gbase + " width=100px/></td>";

                        $("#tb tr").append(trow);

                        imagesloaded = parseInt(imagesloaded) + 1;

                        //alert(imagesloaded +" "+totalimages);
                        if (imagesloaded == totalimages)
                        {
                            $("#loadingdiv").addClass("c-visible");
                             $("#ContentPlaceHolder1_lbluploadmessage").text("Loading complete. Kindly upload and save image.");
                        }
                    });

                }

               

            };

            function loadGif(btnID) {

                //$("#progresdiv").removeClass("c-visible");

                Page_IsValid = null;
                if (typeof (Page_ClientValidate) == 'function') {
                    Page_ClientValidate();
                }
                var btn = document.getElementById(btnID);
                var isValidationOk = Page_IsValid;

                if (isValidationOk !== null) {
                    if (isValidationOk) {
                        $("#progresdiv").removeClass("c-visible");
                        btn.disabled = true;

                    } else {
                        btn.disabled = false;

                    }
                }
                else {


                    $("#progresdiv").removeClass("c-visible");
                    btn.disabled = true;


                }


            }





        </script> 
</asp:Content>
