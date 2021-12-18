using System.IO;


namespace CoreSB.Infrastructure.IO.Settings
{
    using CoreSB.Infrastructure.IO.Serialization;

    /// <summary>
    /// Contains class hierarhy for varialbes JSON serialization, deserialization
    /// and property chanining usage
    /// </summary>
    public class _variables
    {
        public _messages Messages = new _messages();
        public class _messages
        {
            public _serviceMessages SrviceMessages = new _serviceMessages();
            public class _serviceMessages
            {
                public string TestMessage;
                public string ServiceIsNull;
            }

            public _infoMessageTypes InfoMessageTypes = new _infoMessageTypes();
            public class _infoMessageTypes
            {
                public string Success;
                public string Error;
                public string ModelValidationError;
                public string Information;
                public string EntityNotFound;
                public string EntityAllreadyExistFound;
            }

            public _actionTypes ActionTypes = new _actionTypes();
            public class _actionTypes
            {
                public string onCreation;
                public string onUpdate;
                public string onGet;
                public string onDelete;
            }
        }
    }

    /// <summary>
    /// Class for default variable values initialization and saving loading json file
    /// binding string to class for convenient incode usage with serialization and avoidance of enums
    /// </summary>
    public class MessagesInitialization
    {
        public static MessagesInitialization _this;
        static JSONNewtonsoft serialization = new JSONNewtonsoft();
        public static _variables Variables = new _variables();
        string path = $"{Directory.GetCurrentDirectory()}\\variables.json";
        public static void Init()
        {
            _this = new MessagesInitialization();
        }
        public MessagesInitialization()
        {

            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                Variables = serialization.DeSerialize<_variables>(text);
            }
            else
            {
                init();
                File.WriteAllText($"{Directory.GetCurrentDirectory()}\\variables.json", serialization.Serialize(Variables));
            }
        }

        void init()
        {
            Variables.Messages.SrviceMessages.TestMessage = @"inited test message";
            Variables.Messages.SrviceMessages.ServiceIsNull = @"service is null";

            Variables.Messages.InfoMessageTypes.Success = @"Success";
            Variables.Messages.InfoMessageTypes.Error = @"Error";
            Variables.Messages.InfoMessageTypes.ModelValidationError = @"ModelValidationError";
            Variables.Messages.InfoMessageTypes.Information = @"Information";
            Variables.Messages.InfoMessageTypes.EntityNotFound = @"Entity not found";
            Variables.Messages.InfoMessageTypes.EntityAllreadyExistFound = @"Entity allready exist";

            Variables.Messages.ActionTypes.onCreation = @"creating";
            Variables.Messages.ActionTypes.onUpdate = @"updating";
            Variables.Messages.ActionTypes.onGet = @"reading";
            Variables.Messages.ActionTypes.onDelete = @"deleting";
        }
    }

    public class MessagesComposite
    {

        //message examples
        //exception while adding Currencies in dbcurrencyRead
        //information while updating Currencies in dbcurrencyRead
        //successfully updated while updating Currencies in dbcurrencyRead


        /// <summary>
        /// Messaging concatenation for most frequent use casess
        /// </summary>
        /// <param name="infoMessageType">
        /// //info,error,exception, successfully, cannot, unsuccessfull
        /// </param>
        /// <param name="action">
        /// CRUDing
        /// </param>
        /// <param name="entityType">
        /// entity type name
        /// </param>
        /// <param name="dbName">
        /// database name
        /// </param>
        /// <returns></returns>
        public static string messageActionTypeDb(string infoMessageType, string action, string entityType, string dbName)
        {
            var infoStr = !string.IsNullOrEmpty(infoMessageType) ? $"{infoMessageType}" : string.Empty;
            var actionStr = !string.IsNullOrEmpty(action) ? $" while {action}" : string.Empty;
            var entityTypeStr = !string.IsNullOrEmpty(entityType) ? $" with {entityType}" : string.Empty;
            var dbNameStr = !string.IsNullOrEmpty(dbName) ? $" on database {dbName}" : string.Empty;

            var result = $"{infoStr}{actionStr}{entityTypeStr}{dbNameStr}";
            return result;
        }

        public static string ModelValidationErrorOnCreate(string type, string database)
        {
            return MessagesComposite.messageActionTypeDb(
            MessagesInitialization.Variables.Messages.InfoMessageTypes.ModelValidationError,
            MessagesInitialization.Variables.Messages.ActionTypes.onCreation, type,
            database);
        }

        public static string EntityAllreadyExists(string type, string database)
        {
            return MessagesComposite.messageActionTypeDb(
                    MessagesInitialization.Variables.Messages.InfoMessageTypes.EntityAllreadyExistFound,
                    MessagesInitialization.Variables.Messages.ActionTypes.onCreation, type,
                   database);
        }
        public static string EntitySuccessfullyCreated(string type, string database)
        {
            return MessagesComposite.messageActionTypeDb(
                    MessagesInitialization.Variables.Messages.InfoMessageTypes.Success,
                    MessagesInitialization.Variables.Messages.ActionTypes.onCreation, type,
                   database);
        }
        public static string EntityNotFoundOnCreation(string type, string database)
        {
            return MessagesComposite.messageActionTypeDb(
                    MessagesInitialization.Variables.Messages.InfoMessageTypes.EntityNotFound,
                    MessagesInitialization.Variables.Messages.ActionTypes.onCreation, type,
                   database);
        }
        public static string EntityNotFoundOnDelete(string type, string database)
        {
            return MessagesComposite.messageActionTypeDb(
                    MessagesInitialization.Variables.Messages.InfoMessageTypes.EntityNotFound,
                    MessagesInitialization.Variables.Messages.ActionTypes.onDelete, type,
                   database);
        }
        public static string EntityNotFoundOnUpdate(string type, string database)
        {
            return MessagesComposite.messageActionTypeDb(
                    MessagesInitialization.Variables.Messages.InfoMessageTypes.EntityNotFound,
                    MessagesInitialization.Variables.Messages.ActionTypes.onUpdate, type,
                   database);
        }

        public static string EntityDeleted(string type, string database)
        {
            return MessagesComposite.messageActionTypeDb(
                    MessagesInitialization.Variables.Messages.InfoMessageTypes.Success,
                    MessagesInitialization.Variables.Messages.ActionTypes.onDelete, type,
                   database);
        }
        public static string EntityModified(string type, string database)
        {
            return MessagesComposite.messageActionTypeDb(
                    MessagesInitialization.Variables.Messages.InfoMessageTypes.Success,
                    MessagesInitialization.Variables.Messages.ActionTypes.onUpdate, type,
                   database);
        }

    }

}
