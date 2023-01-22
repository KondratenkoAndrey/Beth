namespace Beth.SharedKernel.EventBus.Commands;

public record SendOneTimeCodeCommand(int code, string mobilePhone) : BaseCommand;