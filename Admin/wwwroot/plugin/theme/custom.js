/* Add here all your JS customizations */
var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
  return new bootstrap.Tooltip(tooltipTriggerEl)
})
// get CSS to force reload
var h, a, f;
    a = document.getElementsByTagName('link');
    for (h = 0; h < a.length; h++) {
      f = a[h];
      // if (f.rel.toLowerCase().match(/stylesheet/) && f.href) {
      //   var g = f.href.replace(/(&|\?)rnd=\d+/, '');
      //   f.href = g + (g.match(/\?/) ? '&' : '?');
      //   f.href += 'rnd=' + (new Date().valueOf());
      // }
    } // for