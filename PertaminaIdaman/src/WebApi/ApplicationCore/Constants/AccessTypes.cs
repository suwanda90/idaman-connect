using System.ComponentModel;

namespace ApplicationCore.Constants
{
    public enum AccessTypes
    {
        [field: Description("view")]
        View,

        [field: Description("add")]
        Add,

        [field: Description("edit")]
        Edit,

        [field: Description("delete")]
        Delete,

        [field: Description("detail")]
        Detail,

        [field: Description("approve")]
        Approve,

        [field: Description("reject")]
        Reject,

        [field: Description("postponed")]
        Postponed,

        [field: Description("cancel")]
        Cancel,

        [field: Description("add.document")]
        AddDocument,

        [field: Description("edit.document")]
        EditDocument,

        [field: Description("delete.document")]
        DeleteDocument,

        [field: Description("add.pic")]
        AddPic,

        [field: Description("edit.pic")]
        EditPic,

        [field: Description("delete.pic")]
        DeletePic,

        [field: Description("add.owner")]
        AddOwner,

        [field: Description("edit.owner")]
        EditOwner,

        [field: Description("delete.owner")]
        DeleteOwner,

        [field: Description("add.customer.contact")]
        AddCustomerContact,

        [field: Description("edit.customer.contact")]
        EditCustomerContact,

        [field: Description("delete.customer.contact")]
        DeleteCustomerContact
    }
}
