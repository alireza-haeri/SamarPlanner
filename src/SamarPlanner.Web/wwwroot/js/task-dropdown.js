window.taskDropdown = {
    _handler: null,

    init(dotNetRef, menuRef, wrapperRef) {
        this.dispose(dotNetRef);

        const handler = (e) => {
            if (menuRef && !menuRef.contains(e.target)) {
                dotNetRef.invokeMethodAsync('CloseFromJS');
            }
        };

        setTimeout(() => {
            document.addEventListener('click', handler);
        }, 10);

        this._handler = handler;
        this._dotNetRef = dotNetRef;

        // Position menu to stay within viewport
        this.reposition(menuRef, wrapperRef);
    },

    reposition(menuRef, wrapperRef) {
        if (!menuRef) return;

        // Reset positioning first
        menuRef.style.right = '';
        menuRef.style.left = '';
        menuRef.style.top = '';
        menuRef.style.bottom = '';

        const rect = menuRef.getBoundingClientRect();
        const viewportW = window.innerWidth;
        const viewportH = window.innerHeight;

        // Horizontal: if menu overflows right edge, flip to left-align
        if (rect.right > viewportW - 8) {
            menuRef.style.right = 'auto';
            menuRef.style.left = '0';
        }

        // Horizontal: if menu overflows left edge, pin to left edge
        if (rect.left < 8) {
            menuRef.style.left = '8px';
            menuRef.style.right = 'auto';
        }

        // Vertical: if menu overflows bottom edge, show above the button
        if (rect.bottom > viewportH - 8) {
            menuRef.style.top = 'auto';
            menuRef.style.bottom = 'calc(100% + 6px)';
        }

        // Vertical: if menu overflows top edge, pin to top
        if (rect.top < 8) {
            menuRef.style.top = '8px';
            menuRef.style.bottom = 'auto';
        }
    },

    dispose() {
        if (this._handler) {
            document.removeEventListener('click', this._handler);
            this._handler = null;
        }
    }
};
