/*
## This Windows shim which will be loaded into the comparison graph at runtime.
## This allows us to keep our JS bridging in JS instead of wrapped into C# strings,
## while simultaneously allowing us to avoid polluting the comparison graph too much.
*/


(function() {
  var delegate;

  delegate = {
    callbacks: {},
    callbackKey: function(ref, sha) {
      return '' + ref + ':' + sha;
    },
    getCommitsBefore: function(name, sha, callback) {
      var e, string;
      if (sha == null) {
        return callback([]);
      }
      try {
        string = window.viewModel.getCommitsBefore(name, sha);
        window.InteractionDelegate.callbacks[this.callbackKey(name, sha)] = callback;
        eval(string);
      } catch (_error) {
        e = _error;
        alert('' + e.name + ': ' + e.message + '\n' + e.stack);
      }
    },
    didGetCommitsBefore: function(name, sha, commits) {
      var e, key;
      try {
        key = this.callbackKey(name, sha);
        window.InteractionDelegate.callbacks[key](commits);
        delete window.InteractionDelegate.callbacks[key];
      } catch (_error) {
        e = _error;
        alert('' + e.name + ': ' + e.message + '\n' + e.stack);
      }
    },
    showPopover: function(data, left, top, width, height) {
      viewModel.showPopoverCallback(data, left, top, width, height);
    },
    performAction: function(action, left, top, width, height) {
      if (action === 'sync') {
        ComparisonGraph.performAction('Syncing');
        viewModel.syncCallback(left, top, width, height);
      } else if (action === 'update') {
        ComparisonGraph.performAction('Updating');
        viewModel.updateCallback(left, top, width, height);
      } else if (action === 'branch') {
        viewModel.viewBranchCallback(left, top, width, height);
      } else if (action === 'compare') {
        viewModel.showPopoverCallback(null, left, top, width, height);
      } else {
        alert('performAction called with unknown action: ' + action);
      }
    },
    didPerformAction: function(action, value) {
      if (action !== 'sync' && action !== 'update') {
        alert('didPerformAction called with unknown action: ' + action);
      }
      ComparisonGraph.actionComplete();
    },
    selectCommit: function(branchName, sha) {
      viewModel.selectCommit(branchName, sha);
    },
    hover: function(branchName, sha, left, top) {
      viewModel.hoverCallback(branchName, sha, left, top);
    },
    clearHover: function() {
      viewModel.clearHoverCallback();
    },
    showCommitList: function() {
      alert('showCommitList is unimplemented');
    }
  };

  window.BranchDelegate = delegate;

  window.InteractionDelegate = delegate;

  $(document).on('mousedown', 'a', function(e) {
    return $(document).one('mouseup', function() {
      return $(e.target).blur();
    });
  });

}).call(this);
