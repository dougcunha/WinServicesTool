# Requirements — WinServicesTool

This document lists the functional and non-functional requirements for WinServicesTool. Use this as a reference for design, implementation, code reviews and tests.

## Table of Contents

- [Functional Requirements](#functional-requirements)
- [Non-functional Requirements](#non-functional-requirements)
- [Platform & Environment Requirements](#platform--environment-requirements)
- [Dependencies / Libraries](#dependencies--libraries)
- [Usability & UI Requirements](#usability--ui-requirements)
- [Security & Permissions](#security--permissions)
- [Data and File Formats](#data-and-file-formats)
- [Testing Requirements](#testing-requirements)

---

## Functional Requirements

1. Service Discovery
   - App must list all Windows services installed on the local machine.
   - Each row must display at least: Service Name, Display Name, Status, Startup Type.

2. Service Control
   - App must support Start, Stop and Restart operations for services where allowed.
   - Operations must show progress and return clear success/failure messages.

3. Filtering & Sorting
   - App must provide text-based search (Service Name and Display Name).
   - App must allow filtering by status (Running, Stopped, Paused) and startup type.
   - App must support sorting by columns (Name, Status, Startup Type).

4. Edit Service Properties
   - App should allow changing service startup type and display name when the OS and service allow it.
   - Provide validation and confirmation dialogs.

5. Remove / Uninstall
   - App should provide an option to remove/uninstall services when permitted by Windows.
   - Removal must include multiple confirmations and an option to cancel.

6. Saved Services (Favorites)
   - App must allow users to save a list of services for quick access.
   - Saved lists must be viewable in a dedicated "Saved Services" tab.

7. Export / Import
   - The app must support exporting saved lists to a JSON file and importing them back.
   - Import must validate entries and gracefully skip or report missing services.

8. Logging
   - App must record operational events and errors to log files using NLog.
   - Provide an option to view or export logs for troubleshooting.

---

## Non-functional Requirements

1. Performance
   - The UI must remain responsive during service enumeration and control operations.
   - Use asynchronous programming and background threads to avoid UI blocking.
   - Service list refresh operations should be cancelable.

2. Reliability
   - Operations must handle common failure modes: timeouts, permission denied, service already in desired state.
   - Retry logic should be applied where it makes sense (configurable, limited retries).

3. Maintainability
   - Use dependency injection to decouple UI from business logic (Microsoft.Extensions.DependencyInjection).
   - Keep logic testable; isolate OS-dependent code behind interfaces to allow mocking.

4. Observability
   - Structured logging with sufficient context for debugging (no PII).
   - Adjustable log level via `nlog.config`.

5. Security
   - The app must not attempt privileged operations without explicit user consent/elevation.
   - Avoid logging sensitive data (service account passwords etc.).

6. Localization-ready
   - UI strings must be stored in resources to allow easy localization (PT-BR/EN as initial targets).

7. Packaging
   - Build artifacts should be reproducible and easy to produce via CI (GitHub Actions recommended).

---

## Platform & Environment Requirements

- Operating System: Windows 10 or later (desktop)
- Runtime: .NET 10.0 Desktop runtime (net10.0-windows)
- Visual Studio (recommended) for development: VS 2022 / VS 2025 or latest compatible

---

## Dependencies / Libraries

The project should use (or continue to use) the following libraries and frameworks:

Functional libraries

- `System.ServiceProcess.ServiceController` — control and query Windows services (already referenced in project).

Infrastructure & DI

- `Microsoft.Extensions.DependencyInjection` — dependency injection container.

Logging

- `NLog` and `NLog.Extensions.Logging` — structured logging and integration with Microsoft logging abstractions.

Build-time / Code-generation

- `Fody` and `PropertyChanged.Fody` — property change code generation (used for INotifyPropertyChanged convenience).

Serialization

- `System.Text.Json` — for serializing saved services lists and configuration.

UI

- Windows Forms (UseWindowsForms=true in project) — primary UI framework.

Notes

- Keep all third-party packages reasonably up-to-date; pin versions in CI if reproducible builds are required.

---

## Usability & UI Requirements

- Asynchronous operations: All long-running operations (service enumeration, Start/Stop/Restart, Export/Import) must be asynchronous and not block the UI thread.
- Progress & feedback: Show a progress indicator and a cancel button when appropriate. Report success or failure in a user-friendly message box and log details.
- Error handling: Provide clear, actionable error messages and a link to open logs or copy diagnostic information.
- Accessibility: Follow basic accessibility practices (keyboard navigation, readable contrast, scalable fonts).
- State preservation: Remember last used filters, sorts and window size between sessions (user settings).

---

## Security & Permissions

- The application must detect if it runs with administrator privileges and clearly indicate the current privilege level.
- When not elevated, the UI must operate in a read-only mode for operations that require elevation and offer guidance to restart elevated.
- Minimize privileges: Only perform privileged operations when explicitly requested by the user.

---

## Data and File Formats

- Saved lists (favorites) format:

```json
[  {
   "ServiceName": "MyService",
   "DisplayName": "My Service",
   "Notes": "Optional notes"
  }
]
```

- Logs: plaintext log files managed by NLog; also provide an exported zipped diagnostic bundle when requested.

---

## Testing Requirements

- Unit tests: Service discovery, saved lists serialization/deserialization, and business logic must be covered by unit tests.
- Integration tests: Where possible, add tests that use mocked System.ServiceProcess implementations to validate control flows.
- CI: Configure automated runs of unit tests as part of the build pipeline.

---

## Appendix: Developer notes

- Prefer `System.Text.Json` over `Newtonsoft.Json` unless advanced serialization features are required.
- Abstract system calls to `ServiceController` behind an interface `IServiceManager` to allow mocking and easier testing.
- Consider adding a small wrapper service for complex operations to simplify UI code and improve testability.

---
