<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crud-Operations.aspx.cs" UnobtrusiveValidationMode="None" Inherits="Employee_CRUD_Operations.Crud_Operations" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Crud Operation</title>
 <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-datepicker@1.9.0/dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />

</head>

<body>
    <form id="form1" runat="server">
        
    <div class="container">
        <div class="form-group">
            <div class="col-sm-12">
                <h2 style="text-align: center ; color: black">Employee Crud Operation Form</h2>
            </div>
        </div>
        <hr />
        <div class="row justify-content-center"><!-- Center-align the form content -->
            <div class="form-group col-md-6">
                <label for="employee_id" class="form-label">Employee ID</label>
                <asp:TextBox ID="employee_id" runat="server" class="form-control" placeholder="Employee ID" Text='<%# Bind("EmployeeId") %>'></asp:TextBox>
            </div>
            <div class="form-group col-md-6">
                <label for="employee_name" class="form-label">Employee Name</label>
                <asp:TextBox ID="employee_name" runat="server" class="form-control" placeholder="Employee Name" Text='<%# Bind("EmployeeName") %>'></asp:TextBox>
            </div>
     <div class="form-group col-md-6">
    <label for="employee_email_id" class="form-label">Employee Email ID</label>
    <asp:TextBox ID="employee_email_id" runat="server" class="form-control" placeholder="Employee Email ID" Text='<%# Bind("EmployeeEmail") %>'></asp:TextBox>
    <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ControlToValidate="employee_email_id"
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid email address"
        Display="Dynamic" CssClass="text-danger" />
</div>

 <div class="form-group col-md-6">
    <label for="employee_mobile_no" class="form-label">Employee Mobile No</label>
    <asp:TextBox ID="employee_mobile_no" runat="server" class="form-control" MaxLength="10"  placeholder="Employee Mobile No" Text='<%# Bind("EmployeeMobileNo") %>'></asp:TextBox>
    <div id="mobileNumberErrorMessage" style="color: red;"></div>
    <asp:RegularExpressionValidator ID="RegularExpression1" runat="server" ControlToValidate="employee_mobile_no" ValidationExpression="\d+" ErrorMessage="Only numeric values allowed " Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
</div>


            <div class="form-group col-md-4 ">
                <label for="employee_dob" class="form-label" > Employee DOB</label>
            <asp:TextBox ID="employee_dob" runat="server" class="form-control" placeholder="Employee DOB" Text='<%# Bind("EmployeeDOB") %>'></asp:TextBox>

            </div>
        </div>
    <div class="col-4">
    <asp:Button ID="insertButton" runat="server" class="btn btn-primary" Text="Add" OnClientClick="return validateInputs(this.id);" OnClick="InsertDetails" />
    <asp:Button ID="updateButton" runat="server" class="btn btn-primary" Text="Update" OnClientClick="return validateInputs(this.id);" OnClick="UpdateDetails" />
    <asp:Button ID="DeleteButton" runat="server" class="btn btn-danger" Text="Delete" OnClientClick="return validateInputs(this.id);" OnClick="DeleteRecords" />
</div>

    </div>
       
        
        <asp:GridView ID="gvEmployee" runat="server" CssClass="table table-striped table-bordered">
    <Columns>
        <asp:BoundField DataField="Employee_ID" HeaderText="Employee ID" />
        <asp:BoundField DataField="Employee_Name" HeaderText="Employee Name" />
        <asp:BoundField DataField="Employee_Email_ID" HeaderText="Email" />
        <asp:BoundField DataField="Employee_Mobile_No" HeaderText="Mobile No" />
        <asp:BoundField DataField="Employee_DOB" HeaderText="Date of Birth" DataFormatString="{0:yyyy-MM-dd}" />
    </Columns>
    <HeaderStyle CssClass="thead-dark" />
</asp:GridView>

    </form>


  


</body>
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap-datepicker@1.9.0/dist/js/bootstrap-datepicker.min.js"></script>
<script>
    $(document).ready(function () {
        $('#<%=employee_dob.ClientID %>').datepicker({
            format: 'yyyy-mm-dd', // Set desired date format
            autoclose: true, // Close the datepicker when a date is selected
            todayHighlight: true, // Highlight today's date
            orientation: 'bottom'
        });
    });

   

    function validateInputs(buttonId) {
        var inputControlIds = ['<%= employee_id.ClientID %>', '<%= employee_name.ClientID %>', '<%= employee_email_id.ClientID %>', '<%= employee_mobile_no.ClientID %>', '<%= employee_dob.ClientID %>'];

    var allInputsValid = true;

    // Check if the delete button was clicked
    var deleteButtonClicked = buttonId === '<%= DeleteButton.ClientID %>';

    // If delete button was clicked, only validate employee ID
    if (deleteButtonClicked) {
            inputControlIds = ['<%= employee_id.ClientID %>'];
        }

        // Loop through input controls and validate
        for (var i = 0; i < inputControlIds.length; i++) {
            var inputControl = document.getElementById(inputControlIds[i]);

            if (!inputControl || inputControl.value.trim() === '') {
                alert('Please fill all fields.');
                allInputsValid = false;
                break;
            }
        }

        return allInputsValid;
    }



</script>

</html>
