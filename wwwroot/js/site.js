// Sidebar toggle (mobile)
(function () {
    var toggle = document.getElementById('sidebarToggle');
    var sidebar = document.getElementById('appSidebar');
    var backdrop = document.getElementById('sidebarBackdrop');

    function closeSidebar() {
        if (sidebar) sidebar.classList.remove('open');
        document.body.classList.remove('sidebar-open');
    }
    function openSidebar() {
        if (sidebar) sidebar.classList.add('open');
        document.body.classList.add('sidebar-open');
    }

    if (toggle) {
        toggle.addEventListener('click', function () {
            if (sidebar && sidebar.classList.contains('open')) {
                closeSidebar();
            } else {
                openSidebar();
            }
        });
    }
    if (backdrop) {
        backdrop.addEventListener('click', closeSidebar);
    }

    // Highlight the active nav link based on current path
    var links = document.querySelectorAll('.app-sidebar .nav-link');
    var path = window.location.pathname.toLowerCase();
    var bestMatch = null;
    var bestLen = 0;
    links.forEach(function (link) {
        var href = (link.getAttribute('href') || '').toLowerCase();
        if (href && href !== '/' && path.indexOf(href) === 0 && href.length > bestLen) {
            bestMatch = link;
            bestLen = href.length;
        }
        if (href === '/' && (path === '/' || path.indexOf('/home') === 0)) {
            if (bestLen === 0) { bestMatch = link; }
        }
    });
    if (bestMatch) bestMatch.classList.add('active');
})();
