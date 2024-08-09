namespace domain.Records;

public record RoleBindUser(long roleId, List<long> userIds);