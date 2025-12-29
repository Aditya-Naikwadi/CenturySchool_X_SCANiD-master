<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Admin_Event.aspx.cs" Inherits="CenturyRayonSchool.Admin_Event" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Events</title>
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
         <asp:ScriptManager ID="SCPTMGR" runat="server" EnableCdn="true"></asp:ScriptManager>
              <div class="row mb-2">
                        <div class="col-md-12">
                             <asp:Label ID="lblformtitle" Text="Add Events Data" runat="server" CssClass="font-weight-bolder " style="color:navy;font-size:xx-large;" />
                        </div>
                           
                    </div>
       
              <div class="row">
                           <div class="col-md-4">
                               <asp:Label ID="Label1" runat="server" Text="Select Start Event Date"></asp:Label>
                               <asp:TextBox runat="server"  class="fromdatepicker form-control" ID="txtstartdate" ></asp:TextBox>
                              <asp:RequiredFieldValidator ID="requiredstartdate" runat="server" ErrorMessage="Kindly Select Start Date format" ControlToValidate="hiddenstartdatetext" ForeColor="Red"></asp:RequiredFieldValidator>
                               <asp:TextBox runat="server" ID="hiddenstartdatetext" Text="" type="hidden"></asp:TextBox>
                           </div>
                            
                             <div class="col-md-4">
                               <asp:Label ID="Label6" runat="server" Text="Select End Event Date"></asp:Label>
                               <asp:TextBox runat="server"  class="fromdatepicker form-control" ID="txtenddate"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="requiredenddate" runat="server" ErrorMessage="Kindly Select End Date format" ControlToValidate="hiddenenddatetext" ForeColor="Red"></asp:RequiredFieldValidator>
                               <asp:TextBox runat="server" ID="hiddenenddatetext" Text="" type="hidden"></asp:TextBox>
                           </div>
                           <div class="col-md-4">
                                  <asp:Label ID="topic" runat="server" Text="Event Name"></asp:Label>

                                <asp:TextBox ID="txteventname" runat="server" class="form-control"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="requiredeventname" runat="server" ErrorMessage="Kindly Enter Event Name" ControlToValidate="txteventname" ForeColor="Red"></asp:RequiredFieldValidator>
                               </div>
                  <div class="col-md-4">
                    <asp:Label ID="Label5" runat="server" Text="Event Venue"></asp:Label>
                     
                    <asp:TextBox ID="txteventvenue" runat="server" class="form-control"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="requiredtxteventvenue" runat="server" ErrorMessage="Kindly Enter Event Venue" ControlToValidate="txteventvenue" ForeColor="Red"></asp:RequiredFieldValidator>
                 </div>
                       </div>
                 
                 

             <div class="row mt-1">
                 <div class="col-md-6">
                     
                     <label>Event Description</label>
                     <textarea runat="server" class="form-control" id="txtdescription" rows="5"></textarea>
                     <asp:RequiredFieldValidator ID="requiredtxtdescip" runat="server" ErrorMessage="Kindly Enter Topic Description" ControlToValidate="txtdescription" ForeColor="Red"></asp:RequiredFieldValidator>
                 </div>
                 
                 <div class="col-md-4">

                    <div style="display:flex;">
                         <div style="width:150px;">
                          <asp:Label ID="Label3" runat="server" Text="Start Time"></asp:Label>
                             
                               <asp:TextBox type="time" runat="server"  class="form-control" ID="txtStarttime"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="requiredstarttime" runat="server" ErrorMessage="Kindly Enter Start Time" ControlToValidate="txtStarttime" ForeColor="Red"></asp:RequiredFieldValidator>
                               <%--<asp:TextBox runat="server" ID="hiddenstarttime" Text=""></asp:TextBox>--%>
                        </div>
                        <div  style="width:50px;">
                           <div class="mt-4 text-center">To</div>                            
                        </div>
                    <div style="width:150px;">
                          <asp:Label ID="Label4" runat="server" Text="End Time"></asp:Label>
                               <asp:TextBox type="time" runat="server"  class="form-control" ID="txtEndtime"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="requiredendtime" runat="server" ErrorMessage="Kindly Enter End Time" ControlToValidate="txtEndtime" ForeColor="Red"></asp:RequiredFieldValidator>
                               <%--<asp:TextBox runat="server" ID="hiddenendtime" Text="" ></asp:TextBox>--%>
                    </div>
                    </div>
                   
                             
               </div>
                          

              </div>

           <div class="row mt-1">

                
                <div class="col-md-4 ">
                             
                                  
                                  <label class="font-weight-bold text-primary">Thumbnail Image</label> <br />    
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">  
                                        <ContentTemplate>  
                                            <asp:FileUpload ID="thumbFileupload1" runat="server" />  
                                            
                                        </ContentTemplate>  
                                        <Triggers>  
                                            <asp:PostBackTrigger ControlID="btnSaveData"/>  
                                        </Triggers>  
                               </asp:UpdatePanel>
                           
                        </div>

                <div class="col-md-4 ">
                             
                                  
                                  <label class="font-weight-bold text-primary"> File Selection</label> <br />    
                                <asp:UpdatePanel ID="UpdimageUpload" runat="server">  
                                        <ContentTemplate>  
                                            <asp:FileUpload ID="Fileupload2" runat="server" />  
                                            
                                        </ContentTemplate>  
                                        <Triggers>  
                                            <asp:PostBackTrigger ControlID="btnSaveData"/>  
                                        </Triggers>  
                               </asp:UpdatePanel>
                           
                        </div>   
               </div>

            <div class="row mt-3">
                      <div class="col-md-4">
                          <asp:Button ID="btnSaveData" class="btn btn-success" Text="Save Events" runat="server" OnClick="btnSaveData_Click" OnClientClick="loadGif(this.id)" UseSubmitBehavior="false"/>  
                          <asp:Button ID="btnreset" class="btn btn-primary" Text="Reset" runat="server" OnClick="btnreset_Click" CausesValidation="False" /> 
                      </div>
                       <div class="col-md-12">
                           <asp:Label ID="lbluploadmessage" Text="" runat="server" style="font-size:large;color:blue;" />
                       </div>
                  </div>
             <div class="row mt-5 c-visible" id="progresdiv">
                          <div class="col-md-12">
                              <div style="background-color:lavender;width:100%;text-align:center;">
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
                $("#menu_addevents").addClass("active");
                $("#menu_addevents").css("background-color", "white");
                $("#menu_addevents").css("color", "black");



                var date = new Date();
                var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());

                var month = (date.getMonth() + 1).toString();
                if (month.length == 1) { month = "0" + month; }

                var texttoday = date.getFullYear() + "/" + month + "/" + date.getDate();


                //set start date
                $('#ContentPlaceHolder1_txtstartdate').datepicker({
                    format: 'dd/mm/yyyy'
                }).on('change', function () {
                    $('.datepicker').hide();

                    var fdate = $("#ContentPlaceHolder1_txtstartdate").val();
                    var fdt = fdate.split('/');
                    fdate = fdt[2] + "/" + fdt[1] + "/" + fdt[0];
                    $("#ContentPlaceHolder1_hiddenstartdatetext").val(fdate);
                });


                $('#ContentPlaceHolder1_txtstartdate').datepicker('setDate', today);

                $("#ContentPlaceHolder1_hiddenenddatetext").val(texttoday);



                //set end date
                $('#ContentPlaceHolder1_txtenddate').datepicker({
                    format: 'dd/mm/yyyy'
                }).on('change', function () {
                    $('.datepicker').hide();

                    var fdate = $("#ContentPlaceHolder1_txtenddate").val();
                    var fdt = fdate.split('/');
                    fdate = fdt[2] + "/" + fdt[1] + "/" + fdt[0];
                    $("#ContentPlaceHolder1_hiddenenddatetext").val(fdate);
                });


                $('#ContentPlaceHolder1_txtenddate').datepicker('setDate', today);

                $("#ContentPlaceHolder1_hiddenenddatetext").val(texttoday);



              

                

               


                

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
