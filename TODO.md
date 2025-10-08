# TODO - WinServicesTool

This file lists features and implementation tasks derived from the project's README. Use it to track development progress.

> Status: working draft. Update tasks, priorities and assignees as work progresses.

---

## High level roadmap

1. Core service discovery and UI
2. Service control (start/stop/restart) with secure handling
3. Filtering, sorting and search
4. Saved services (Favorites) with export/import
5. Editing and removal of services (where permitted)
6. Logging, tests, CI and packaging

---

## Tasks (feature-driven)

### 1) Service listing (Core)

- [x] Implement service enumeration using `System.ServiceProcess.ServiceController`
- [x] Show essential columns: Service Name, Display Name, Status, Startup Type
- [ ] Paging/virtualization for large lists (performance)
- [x] Auto-adjust DataGridView column widths to use available monitor space (Fill and AllCells mode)
- [x] Allow manual column resize and persist column widths between sessions (user settings or JSON)
- Priority: High

### 3) Filter, Search and Sort

- [ ] Text filter by Service Name or Display Name (contains / starts-with)
- [ ] Status filter (Running, Stopped, Paused, All)
- [ ] Sort by Name, Status, Startup Type (clickable column headers)
- [ ] Preserve sorting/filter selections between app sessions (optional)
- Priority: High

### 4) Edit service properties

- [ ] Allow editing Display Name where permitted
- [ ] Allow changing Startup Type (Automatic, Manual, Disabled) where permitted
- [ ] Validate changes and show confirmations
- [ ] Handle permission errors gracefully
- Priority: Medium

### 5) Remove / Uninstall service

- [ ] Provide a controlled flow to uninstall/remove a service (with multiple confirmations)
- [ ] Check permissions and refuse action when not allowed
- [ ] Optionally backup current configuration before removal
- Priority: Low (dangerous)

### 6) Saved Services (Favorites)

- [ ] Add UI to add/remove services to a saved/favorites list
- [ ] Add a dedicated tab that displays only saved services
- [ ] Persist saved lists locally (JSON serialization)
- [ ] Support multiple named lists (profiles)
- Priority: High

### 7) Export / Import saved lists

- [ ] Export favorites list to JSON file (with simple schema)
- [ ] Import favorites list and validate entries (skip missing services)
- [ ] Provide an import preview and undo option
- Priority: Medium

### 8) Logging & Diagnostics

- [ ] Improve NLog configuration and include a UI-accessible log viewer
- [ ] Add telemetry-friendly structured logs (no PII)
- [ ] Include an option to copy or export logs for support
- Priority: Medium

### 9) Permissions & Elevation UX

- [ ] Detect whether the app is running elevated
- [ ] Present a clear indication when running without admin rights
- [ ] Provide quick guidance / button to restart with elevation
- [ ] Disable control actions when not elevated (read-only mode)
- Priority: High

### 10) Tests

- [ ] Unit tests for service discovery and list serialization
- [ ] Integration tests (mocked) for control operations
- [ ] Add test runner to CI
- Priority: Medium

### 11) CI / Packaging / Releases

- [ ] Add GitHub Actions to build on push and PR
- [ ] Produce release artifacts (zip) and optionally installer
- [ ] Tagging and release workflow configured
- Priority: Medium

### 12) Localization

- [ ] Prepare UI for localization (resource files)
- [ ] Add PT-BR and EN translations for core UI strings
- Priority: Low

---

## File format and conventions

- Saved lists: JSON array of objects: { "ServiceName": "", "DisplayName": "", "Notes": "" }
- Tests should avoid requiring elevation; mock System.ServiceProcess where possible.

---

## Example implementation milestones

- Milestone 1 (MVP): Tasks 1, 2, 3, 6, 9
- Milestone 2: Tasks 4, 7, 8, 10
- Milestone 3: Tasks 11, 12

---

## How to update this TODO

Edit this file and use checkboxes to mark progress. Add small, focused subtasks and reference PR numbers when tasks are completed.
