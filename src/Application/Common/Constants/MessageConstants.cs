namespace Application.Common.Constants;

public static class MessageConstants
{
    /* --------------------------------- 200 --------------------------------- */

    public const string SuccessfullyCreated = "Successfully created.";
    public const string SuccessfullyDeleted = "Successfully deleted.";
    public const string SuccessfullyRetrieved = "Successfully retrieved.";
    public const string SuccessfullyUpdated = "Successfully updated.";
    public const string SuccessfullyLoggedIn = "Successfully logged in.";

    /* --------------------------------- 400 --------------------------------- */

    public const string NotFound = "Not Found";
    public const string DuplicateUsername = "Username already taken.";
    public const string InvalidCredentials = "Invalid Username / Email or Password";
    public const string BadRequest = "Invalid Parameters";
    public const string Forbidden = "Forbidden";
    public const string AlreadyLoggedIn = "You are already logged in.";

    /* --------------------------------- 500 --------------------------------- */

    public const string InternalServerError = "Internal Server Error";
    public const string MappingFailed = "{time} - Internal Server Error - {entity} mapping failed inside : {class}";

    public const string EntityCreationFailed =
        "{time} - Internal Server Error - {entity} creation failed inside :  {class}";

    public const string EntityRetrievalFailed =
        "{time} - Internal Server Error - {entity} retrieval failed inside :  {class}";

    public const string EntityDeletionFailed =
        "{time} - Internal Server Error - {entity} deletion failed inside : {class}";

    public const string EntityUpdateFailed = "{time} - Internal Server Error - {entity} update failed inside : {class}";

    public const string EntityRelationshipsRetrievalFailed =
        "{time} - Internal Server Error - {entity} relationships retrieval failed inside : {class}";

    public const string ClaimsRetrievalFailed =
        "{time} - Internal Server Error - Claims retrieval failed while user is logged in inside : {class}";
}