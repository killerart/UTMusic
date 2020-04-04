var stickies = [].slice.call(document.querySelectorAll('.sticky'));

stickies.forEach(function (sticky) {
    if (CSS.supports && CSS.supports('position', 'sticky')) {
        apply_sticky_class(sticky);

        window.addEventListener('scroll', function () {
            apply_sticky_class(sticky);
        })
    }
})

function apply_sticky_class(sticky) {
    var currentOffset = sticky.getBoundingClientRect().top;
    var stickyOffset = parseInt(getComputedStyle(sticky).top.replace('px', ''));
    var isStuck = currentOffset <= stickyOffset;

    sticky.classList.toggle('js-is-sticky', isStuck);
}