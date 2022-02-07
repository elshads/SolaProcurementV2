using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace SolaProcurementV2.Server.Configurations
{
    public class AppState
    {
        private ISnackbar Snackbar { get; set; }
        public AppState(ISnackbar snackbar)
        {
            Snackbar = snackbar;
        }

        public event Action OnBackClick;
        public event Action OnAddClick;
        public event Action OnEditClick;
        public event Action OnSaveClick;
        public event Action OnDeleteClick;
        public event Action OnRefreshClick;
        public event Action OnReportsClick;
        public event Action OnAttachmentsClick;
        public event Action OnApproveClick;
        public event Action OnSuspendClick;
        public event Action OnRejectClick;
        public event Action OnReportProblemClick;
        public event Action OnDocumentationClick;
        public event Action OnDevModeClick;
        public event Action OnPropertiesClick;

        public event Action OnAppStateChanged;

        public void ShowAlert(string message, MudBlazor.Severity severity, Action onClick = null)
        {
            Snackbar.Add(message, severity, config =>
            {
                config.Onclick = snackbar =>
                {
                    config.RequireInteraction = (onClick != null);
                    onClick?.Invoke();
                    return Task.CompletedTask;
                };
            });
        }

        public void BackClick()
        {
            OnBackClick?.Invoke();
        }

        public void AddClick()
        {
            OnAddClick?.Invoke();
        }

        public void EditClick()
        {
            OnEditClick?.Invoke();
        }

        public void SaveClick()
        {
            OnSaveClick?.Invoke();
        }

        public void DeleteClick()
        {
            OnDeleteClick?.Invoke();
        }

        public void RefreshClick()
        {
            OnRefreshClick?.Invoke();
        }

        public void ReportsClick()
        {
            OnReportsClick?.Invoke();
        }

        public void AttachmentsClick()
        {
            OnAttachmentsClick?.Invoke();
        }

        public void ApproveClick()
        {
            OnApproveClick?.Invoke();
        }

        public void SuspendClick()
        {
            OnSuspendClick?.Invoke();
        }

        public void RejectClick()
        {
            OnRejectClick?.Invoke();
        }

        public void ReportProblemClick()
        {
            OnReportProblemClick?.Invoke();
        }

        public void DocumentationClick()
        {
            OnDocumentationClick?.Invoke();
        }

        public void DevModeClick()
        {
            OnDevModeClick?.Invoke();
        }

        public void PropertiesClick()
        {
            OnPropertiesClick?.Invoke();
        }


        PropertiesData propertiesData = new();

        bool isLoading = false;

        bool visible = true;
        bool isBackVisible = true;
        bool isBackDisabled = false;
        bool isAddVisible = true;
        bool isAddDisabled = false;
        bool isEditVisible = false;
        bool isEditDisabled = false;
        bool isSaveVisible = true;
        bool isSaveDisabled = false;
        bool isDeleteVisible = true;
        bool isDeleteDisabled = false;
        bool isRefreshVisible = true;
        bool isRefreshDisabled = false;
        bool isReportsVisible = true;
        bool isReportsDisabled = false;
        bool isAttachmentsVisible = true;
        bool isAttachmentsDisabled = false;
        bool isApprovalVisible = true;
        bool isApproveVisible = true;
        bool isApproveDisabled = false;
        bool isSuspendVisible = true;
        bool isSuspendDisabled = false;
        bool isRejectVisible = true;
        bool isRejectDisabled = false;
        bool isReportProblemVisible = true;
        bool isReportProblemDisabled = false;
        bool isDocumentationVisible = true;
        bool isDocumentationDisabled = false;
        bool isDevModeVisible = true;
        bool isDevModeDisabled = false;
        bool isPropertiesVisible = true;
        bool isPropertiesDisabled = false;

        public PropertiesData PropertiesData { get { return (propertiesData != null ? propertiesData : new()); } set { propertiesData = (value != null ? value : new()); OnAppStateChanged?.Invoke(); } }
        public bool IsLoading { get { return isLoading; } set { isLoading = value; OnAppStateChanged?.Invoke(); } }
        public bool Visible { get { return visible; } set { visible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsBackVisible { get { return isBackVisible; } set { isBackVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsBackDisabled { get { return isBackDisabled; } set { isBackDisabled = value; OnAppStateChanged?.Invoke(); } }
        public bool IsAddVisible { get { return isAddVisible; } set { isAddVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsAddDisabled { get { return isAddDisabled; } set { isAddDisabled = value; OnAppStateChanged?.Invoke(); } }
        public bool IsEditVisible { get { return isEditVisible; } set { isEditVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsEditDisabled { get { return isEditDisabled; } set { isEditDisabled = value; OnAppStateChanged?.Invoke(); } }
        public bool IsSaveVisible { get { return isSaveVisible; } set { isSaveVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsSaveDisabled { get { return isSaveDisabled; } set { isSaveDisabled = value; OnAppStateChanged?.Invoke(); } }
        public bool IsDeleteVisible { get { return isDeleteVisible; } set { isDeleteVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsDeleteDisabled { get { return isDeleteDisabled; } set { isDeleteDisabled = value; OnAppStateChanged?.Invoke(); } }
        public bool IsRefreshVisible { get { return isRefreshVisible; } set { isRefreshVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsRefreshDisabled { get { return isRefreshDisabled; } set { isRefreshDisabled = value; OnAppStateChanged?.Invoke(); } }
        public bool IsReportsVisible { get { return isReportsVisible; } set { isReportsVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsReportsDisabled { get { return isReportsDisabled; } set { isReportsDisabled = value; OnAppStateChanged?.Invoke(); } }
        public bool IsAttachmentsVisible { get { return isAttachmentsVisible; } set { isAttachmentsVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsAttachmentsDisabled { get { return isAttachmentsDisabled; } set { isAttachmentsDisabled = value; OnAppStateChanged?.Invoke(); } }
        public bool IsApprovalVisible { get { return isApprovalVisible; } set { isApprovalVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsApproveVisible { get { return isApproveVisible; } set { isApproveVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsApproveDisabled { get { return isApproveDisabled; } set { isApproveDisabled = value; OnAppStateChanged?.Invoke(); } }
        public bool IsSuspendVisible { get { return isSuspendVisible; } set { isSuspendVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsSuspendDisabled { get { return isSuspendDisabled; } set { isSuspendDisabled = value; OnAppStateChanged?.Invoke(); } }
        public bool IsRejectVisible { get { return isRejectVisible; } set { isRejectVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsRejectDisabled { get { return isRejectDisabled; } set { isRejectDisabled = value; OnAppStateChanged?.Invoke(); } }
        public bool IsReportProblemVisible { get { return isReportProblemVisible; } set { isReportProblemVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsReportProblemDisabled { get { return isReportProblemDisabled; } set { isReportProblemDisabled = value; OnAppStateChanged?.Invoke(); } }
        public bool IsDocumentationVisible { get { return isDocumentationVisible; } set { isDocumentationVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsDocumentationDisabled { get { return isDocumentationDisabled; } set { isDocumentationDisabled = value; OnAppStateChanged?.Invoke(); } }
        public bool IsDevModeVisible { get { return isDevModeVisible; } set { isDevModeVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsDevModeDisabled { get { return isDevModeDisabled; } set { isDevModeDisabled = value; OnAppStateChanged?.Invoke(); } }
        public bool IsPropertiesVisible { get { return isPropertiesVisible; } set { isPropertiesVisible = value; OnAppStateChanged?.Invoke(); } }
        public bool IsPropertiesDisabled { get { return isPropertiesDisabled; } set { isPropertiesDisabled = value; OnAppStateChanged?.Invoke(); } }

    }
}
