using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SignalExceptionType {
    /// Attempting to bind more than one value of the same type to a command
    COMMAND_VALUE_CONFLICT,

    /// A Signal mapped to a Command found no matching injectable Type to bind a parameter to.
    COMMAND_VALUE_NOT_FOUND,

    /// SignalCommandBinder attempted to bind a null value from a signal to a Command
    COMMAND_NULL_INJECTION,
}

public class SignalException : Exception {
    public SignalExceptionType type { get; set; }

    public SignalException() : base() {
    }

    /// Constructs a SignalException with a message and SignalExceptionType
    public SignalException(string message, SignalExceptionType exceptionType) : base(message) {
        type = exceptionType;
    }
}