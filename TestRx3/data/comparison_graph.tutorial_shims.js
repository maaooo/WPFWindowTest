(function() {
  var callbackPerTrigger, delegate;

  callbackPerTrigger = {};

  delegate = {
    open: function() {
      return viewModel.open();
    },
    quit: function() {
      return viewModel.quit();
    },
    close: function() {
      return viewModel.close();
    },
    complete: function() {
      return viewModel.complete();
    },
    updatePopupTarget: function(target) {
      return viewModel.updatePopupTarget(target);
    },
    updateHeight: function(height) {
      return viewModel.updateHeight(height);
    },
    registerForTrigger: function(triggerName, callback) {
      callbackPerTrigger[triggerName] = callback;
      return viewModel.registerForTrigger(triggerName);
    },
    triggerFired: function(name) {
      var callback;
      callback = callbackPerTrigger[name];
      callback();
      return delete callbackPerTrigger[name];
    },
    makeLocalChanges: function(fileName, content) {
      return viewModel.makeLocalChanges(fileName, content);
    },
    getTutorialLocation: function() {
      return "go to the gear menu and select Tutorial.";
    }
  };

  window.Delegate = delegate;

}).call(this);
