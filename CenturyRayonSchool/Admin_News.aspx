<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeFile="Admin_News.aspx.cs" Inherits="CenturyRayonSchool.Admin_News" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Notice</title>
     <link href="../css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="../js/bootstrap-datepicker.min.js"></script>

    <style>
      div1-style {
            background-color: #cccccc00; 
            font-family: Arial, Helvetica, sans-serif; 
            font-size: medium; 
            font-weight: normal; 
            font-style: normal;
            /*margin-left:100px;*/
            margin-top:50px;
            /*position:fixed;*/

        }


         .divmessage {
             width: 100%;
             background-color:navy;
              
              color:white;
             
              font-size:larger;
              padding-top:5px;
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
                             <asp:Label ID="lblformtitle" Text="Add Notice" runat="server" CssClass="font-weight-bolder " style="color:navy;font-size:xx-large;" />
                        </div>
                           
                    </div>

              <div class="row">
                           <div class="col-md-4">
                                <asp:Label ID="Label1" runat="server" Text="Select Date"></asp:Label>
                                <%--<input type="date" id="datecal1" class="form-control" name="trip-start" value=""  style="line-height: normal; background-color: #FFFFFF; padding-top: 0px; padding-bottom: -5px; margin-bottom: -1px; display: inline;" />--%>
                               
                               <%--<input type="text" name="fromdate" class="fromdatepicker form-control" id="fromdate" readonly="readonly" placeholder="dd/MM/yyyy" />--%>
                                <asp:TextBox runat="server"  class="fromdatepicker form-control" ID="txtselecteddate" readonly="true"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="requiredfromdate" runat="server" ErrorMessage="Kindly Select Date format" ControlToValidate="hiddendatetext" ForeColor="Red"></asp:RequiredFieldValidator>
                               <asp:TextBox runat="server" ID="hiddendatetext" Text="" type="hidden"></asp:TextBox>
                           </div>
                           <div class="col-md-4">
                                <asp:Label ID="topic" runat="server" Text="Topic Name"></asp:Label>

                                <asp:TextBox ID="txttopicname" runat="server" class="form-control"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="requiredtopicname" runat="server" ErrorMessage="Kindly Enter Topic Name" ControlToValidate="txttopicname" ForeColor="Red"></asp:RequiredFieldValidator>
                               </div>

                       </div>

             <asp:ScriptManager ID="SCPTMGR" runat="server" EnableCdn="true"></asp:ScriptManager>
             
             <div class="row mt-3">
                 <div class="col-md-4">
                     
                     <label>Topic Description</label>
                     
                     <textarea runat="server" class="form-control" id="txtdescription" rows="5"></textarea>
                     <asp:RequiredFieldValidator ID="requiredtxtdescip" runat="server" ErrorMessage="Kindly Enter Topic Description" ControlToValidate="txtdescription" ForeColor="Red"></asp:RequiredFieldValidator>
                 </div>
                 
                 
                         <div class="col-md-4 ">
                             <div class="mt-5">
                                  
                                  <label Class="font-weight-bold text-primary"> File Selection</label> <br />    
                                <asp:UpdatePanel ID="UpdimageUpload" runat="server">  
                                        <ContentTemplate>  
                                            <asp:FileUpload ID="Fileupload1" runat="server" />  
                                            
                                        </ContentTemplate>  
                                        <Triggers>  
                                            <asp:PostBackTrigger ControlID="btnSaveData"/>  
                                        </Triggers>  
                               </asp:UpdatePanel>
                           </div>
                        </div>  
                 

                
              </div>
                  <div class="row mt-3">
                      <div class="col-md-4">
                          <asp:Button ID="btnSaveData" class="btn btn-success" Text="Save News" runat="server" OnClick="btnsavedata_Click" OnClientClick="loadGif(this.id)" UseSubmitBehavior="false"/>  
                          <asp:Button ID="btnreset" class="btn btn-primary" Text="Reset" runat="server" OnClick="btnreset_Click" CausesValidation="False" /> 
                      </div>
                       <div class="col-md-12">
                           <asp:Label ID="lbluploadmessage" Text="" runat="server" style="font-size:large;color:blue;" />
                       </div>
                  </div>
              <div class="row mt-5 c-visible" id="progresdiv">
                          <div class="col-md-12">
                              <div style="background-color:navy;width:100%;text-align:center;color:white;">
                                  <img src="img/giphy.gif" alt="Alternate Text" style="width:50px;height:auto;" />
                                  <asp:Label ID="Label2" Text="Saving Data ..." runat="server" style="font-size:large;" />
                              </div>
                              
                          </div>
                      </div>
                
           
 </div>
                  
   

    <script>
        $(document).ready(function () {

            $("#menu_studentcorner").addClass("show");
            $("#item_studentcorner").addClass("active");
            $("#menu_addnews").addClass("active");
            $("#menu_addnews").css("background-color", "white");
            $("#menu_addnews").css("color", "black");



            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());

            var month = (date.getMonth() + 1).toString();
            if (month.length == 1) { month = "0" + month; }

            var texttoday = date.getFullYear() + "/" + month + "/" + date.getDate();

            $('.fromdatepicker').datepicker({
                format: 'dd/mm/yyyy'
            }).on('change', function () {
                $('.datepicker').hide();

                var fdate = $("#ContentPlaceHolder1_txtselecteddate").val();
                var fdt = fdate.split('/');
                fdate = fdt[2] + "/" + fdt[1] + "/" + fdt[0];
                $("#ContentPlaceHolder1_hiddendatetext").val(fdate);
            });


            $('.fromdatepicker').datepicker('setDate', today);


            $("#ContentPlaceHolder1_hiddendatetext").val(texttoday);

        });



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
