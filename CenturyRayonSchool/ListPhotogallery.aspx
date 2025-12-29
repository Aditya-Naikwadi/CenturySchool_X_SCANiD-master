<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ListPhotogallery.aspx.cs" Inherits="CenturyRayonSchool.ListPhotogallery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

        
        

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid dashboard-content" >
         <div class="row mb-2">
                <asp:Label ID="lblformtitle" Text="Photo Gallery Section" runat="server" CssClass="font-weight-bolder " style="color:navy;font-size:xx-large;" />
        </div>
           <div class="row">
               <div class="col-md-4">
                   <asp:Label ID="Label1" runat="server" Text="Select Gallery Name"></asp:Label>
                   <asp:DropDownList ID="filtergalleryname" runat="server" CssClass="form-control">

                   </asp:DropDownList>
               </div>
               <div class="col-md-2">
                   <asp:Button ID="disablebtn" Text="Disable Sellected Gallery" runat="server" CssClass="btn btn-danger" OnClick="disablebtn_Click" />
               </div>
              <div class="col-md-12 mt-1">
                  <div class="divmessage">
                      <asp:label ID="lblmessage" text="" runat="server" />
                  </div>
                  
              </div>
               <div class="col-md-12">

                   <div class="table-responsive">
                        <asp:GridView ID="photogridview" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" OnRowCommand="photogridview_RowCommand">

                           <Columns> 
                               <asp:boundfield datafield="ID" headertext="ID" ItemStyle-Width="30px"/>

                                 <asp:boundfield datafield="galleryname" headertext="Gallery Name" ItemStyle-Width="30px"/>

                                <asp:boundfield datafield="filename" headertext="File Name" ItemStyle-Width="30px"/>

                                <asp:boundfield datafield="description" headertext="Description" ItemStyle-Width="30px"/>
                                     
                                <asp:boundfield datafield="createddate" headertext="Created Date" ItemStyle-Width="30px"/>

                                 <asp:boundfield datafield="status" headertext="Status" ItemStyle-Width="30px"/>

                               <asp:TemplateField ItemStyle-Width="30px" HeaderText="Image">  
                                <ItemTemplate>  
                                    <asp:Image ID="filepath" runat="server" ImageUrl='<%#Eval("filepath")%>' Width="100"/>
                                </ItemTemplate>  

                            <ItemStyle Width="30px"></ItemStyle>
                            </asp:TemplateField> 


                               <asp:TemplateField ItemStyle-Width="30px" HeaderText="Action">  
                                <ItemTemplate>  
                                    <asp:Button ID="btnremove" Text="Remove" runat="server" CssClass="btn btn-danger" CommandName="removeImage"  CommandArgument="<%# ((GridViewRow) Container).RowIndex%>" OnClientClick="Confirm()" visible='<%# setButtonVisibility(Convert.ToDateTime(Eval("createddate")).ToString("yyyy/MM/dd")) %>'/>
                                </ItemTemplate>  

                            <ItemStyle Width="30px"></ItemStyle>
                            </asp:TemplateField>           
                           </Columns>
                            <HeaderStyle BackColor="Black" ForeColor="White" />
                        </asp:GridView>
                   </div>
                  


               </div>

               

               </div>



    </div>

             

         

   <script>

       $(document).ready(function () {

           $("#menu_photogallery").addClass("show");
           $("#item_photogallery").addClass("active");
           $("#menu_view_photogallery").addClass("active");
           $("#menu_view_photogallery").css("background-color", "white");
           $("#menu_view_photogallery").css("color", "black");

       });


       function Confirm() {
           var confirm_value = document.createElement("INPUT");
           confirm_value.type = "hidden";
           confirm_value.name = "confirm_value";
           if (confirm("Do you want to remove image?")) {
               confirm_value.value = "Yes";
           } else {
               confirm_value.value = "No";
           }
           document.forms[0].appendChild(confirm_value);
       }







    </script>
</asp:Content>
