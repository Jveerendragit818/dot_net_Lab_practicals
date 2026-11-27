
document.addEventListener("DOMContentLoaded", function () {

    console.log("Smart Campus Portal started");

    // Active navigation
    const currentPath = window.location.pathname.toLowerCase();

    document.querySelectorAll(".sidebar a").forEach(link => {
        const linkPath = new URL(link.href).pathname.toLowerCase();

        if (linkPath === currentPath) {
            link.classList.add("active");
        }
    });

});
