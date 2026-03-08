window.liteBrite = {
    getItem: function (key) {
        return localStorage.getItem(key);
    },
    setItem: function (key, value) {
        localStorage.setItem(key, value);
    },
    registerGridKeys: function (dotNetRef) {
        window._lbKeyHandler = function (e) {
            var arrows = ['ArrowUp', 'ArrowDown', 'ArrowLeft', 'ArrowRight'];
            var isCtrlArrow = e.ctrlKey && arrows.includes(e.key);
            var isAction = e.key === 'Delete' || e.key === 'Escape';
            if (!isCtrlArrow && !isAction) return;
            // Don't steal keys from text inputs
            var tag = document.activeElement && document.activeElement.tagName;
            if (tag === 'INPUT' || tag === 'TEXTAREA' || tag === 'SELECT') return;
            if (isCtrlArrow) e.preventDefault();
            dotNetRef.invokeMethodAsync('OnGridKeyDown', e.key, e.ctrlKey);
        };
        document.addEventListener('keydown', window._lbKeyHandler);
    },
    unregisterGridKeys: function () {
        if (window._lbKeyHandler) {
            document.removeEventListener('keydown', window._lbKeyHandler);
            window._lbKeyHandler = null;
        }
    },
    printWithTitle: function (title) {
        var original = document.title;
        document.title = title;
        window.addEventListener('afterprint', function restore() {
            document.title = original;
            window.removeEventListener('afterprint', restore);
        });
        window.print();
    }
};
