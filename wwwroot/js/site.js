// Maal - Marriage Card Game Point Tracker

// Highlight active nav link
document.addEventListener('DOMContentLoaded', function () {
    const currentPath = window.location.pathname.toLowerCase();
    document.querySelectorAll('.app-nav a').forEach(function (link) {
        const href = link.getAttribute('href');
        if (href && currentPath === href.toLowerCase()) {
            link.classList.add('active');
        }
    });
});
