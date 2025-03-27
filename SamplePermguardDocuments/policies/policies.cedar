@id("platform-super-administrator")
permit (
    principal,
    action,
    resource is Resource::folder
)
when {
    principal.isSuperUser == true
};

@id("platform-editor")
permit (
    principal,
    action == Action::"write",
    resource is Resource::folder
)
when {
    principal.groups.contains("editors")
};

@id("platform-viewer")
permit (
    principal,
    action == Action::"read",
    resource is Resource::folder
)
when {
    principal.isAuthenticated == true
};


@id("platform-viewer-simple")
permit (
    principal,
    action == Action::"list",
    resource is Resource::bucket
);