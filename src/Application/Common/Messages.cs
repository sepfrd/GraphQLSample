namespace Application.Common;

public static class Messages
{
    /* --------------------------------- 200 --------------------------------- */
    
    public const string SuccessfullyCreated = "Successfully created.";
    public const string SuccessfullyDeleted = "Successfully deleted.";
    public const string SuccessfullyRetrieved = "Successfully retrieved.";
    public const string SuccessfullyUpdated = "Successfully updated.";
    
    /* --------------------------------- 400 --------------------------------- */

    public const string NotFound = "Not Found";

    /* --------------------------------- 500 --------------------------------- */

    public const string InternalServerError = "Internal Server Error";
    public const string MappingFailed = "{time} - Internal Server Error - Mapping Failed - {type}";
    public const string EntityCreationFailed = "{time} - Internal Server Error - Entity Creation Failed - {type}";
    public const string EntityDeletionFailed = "{time} - Internal Server Error - Entity Deletion Failed - {type}";
    public const string EntityUpdateFailed = "{time} - Internal Server Error - Entity Update Failed - {type}";
}