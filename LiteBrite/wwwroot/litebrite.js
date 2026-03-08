window.liteBrite = {
    getItem: function (key) {
        return localStorage.getItem(key);
    },
    setItem: function (key, value) {
        localStorage.setItem(key, value);
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
