
document.addEventListener('DOMContentLoaded', function () {
    initProgressOverlay();
    initErrorDisplay();
});

/**
 * Manages the global loading overlay during Ajax/Form submissions
 */
function initProgressOverlay() {
    const progressOverlay = document.getElementById('progress');

    // Display loader automatically on standard form submissions
    document.addEventListener('submit', function (e) {
        if (progressOverlay && !e.defaultPrevented) {
            progressOverlay.classList.remove('displaynone');
        }
    });

    // Helper functions exposed globally for ASP.NET AJAX / jQuery requests
    window.showLoadingProgress = function () {
        if (progressOverlay) {
            progressOverlay.classList.remove('displaynone');
        }
    };

    window.hideLoadingProgress = function () {
        if (progressOverlay) {
            progressOverlay.classList.add('displaynone');
        }
    };
}

/**
 * Handles global error container display toggle
 */
function initErrorDisplay() {
    const errorDiv = document.getElementById('error-display-div');

    window.showGlobalError = function (message) {
        if (errorDiv) {
            errorDiv.innerHTML = message;
            errorDiv.classList.remove('displaynone');
        }
    };

    window.clearGlobalError = function () {
        if (errorDiv) {
            errorDiv.innerHTML = '';
            errorDiv.classList.add('displaynone');
        }
    };
}



 
document.addEventListener('DOMContentLoaded', function () {
    initThemeToggle();
});

    /**
     * Modern Dark/Light Mode Switcher using Bootstrap 5 Data Attributes
     */
    function initThemeToggle() {
        var themeToggleBtn = document.getElementById('themeToggle');
        if (!themeToggleBtn) return;

        var sunIcon = themeToggleBtn.querySelector('.theme-icon-sun');
        var moonIcon = themeToggleBtn.querySelector('.theme-icon-moon');

        // Get preferred theme from localStorage or system settings
        function getPreferredTheme() {
            var storedTheme = localStorage.getItem('theme');
            if (storedTheme) {
                return storedTheme;
            }
            return window.matchMedia('(prefers-color-scheme: light)').matches ? 'dark' : 'light';
        }

        // Apply selected theme to HTML tag
        function setTheme(theme) {
            document.documentElement.setAttribute('data-bs-theme', theme);
            localStorage.setItem('theme', theme);

            if (theme === 'dark') {
                if (sunIcon) sunIcon.classList.remove('d-none');
                if (moonIcon) moonIcon.classList.add('d-none');
            } else {
                if (sunIcon) sunIcon.classList.add('d-none');
                if (moonIcon) moonIcon.classList.remove('d-none');
            }
        }

        // Initialize initial theme state
        setTheme(getPreferredTheme());

        // Event listener for toggle click
        themeToggleBtn.addEventListener('click', function () {
            var currentTheme = document.documentElement.getAttribute('data-bs-theme');
            var newTheme = currentTheme === 'dark' ? 'light' : 'dark';
            setTheme(newTheme);
        });
    }

