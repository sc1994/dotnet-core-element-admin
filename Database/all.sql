CREATE DATABASE MainDb
	CHARACTER SET utf8mb4
	COLLATE utf8mb4_0900_ai_ci;

DROP TABLE IF EXISTS MainDb.Article;
CREATE TABLE MainDb.Article (
  Id              INT(11)      NOT NULL    AUTO_INCREMENT,
  Importance      INT(11)      DEFAULT NULL,
  Remark          VARCHAR(255) DEFAULT NULL,
  Timestamp       VARCHAR(255) DEFAULT NULL,
  Title           VARCHAR(255) DEFAULT NULL,
  Status          VARCHAR(255) DEFAULT NULL,
  Type            VARCHAR(255) DEFAULT NULL,
  Author          VARCHAR(255) DEFAULT NULL,
  CommentDisabled VARCHAR(255) DEFAULT NULL,
  Content         VARCHAR(255) DEFAULT NULL,
  ContentShort    VARCHAR(255) DEFAULT NULL,
  DisplayTime     DATETIME     DEFAULT NULL,
  Forecast        DEC(10,10)   DEFAULT NULL,
  ImageUri        VARCHAR(255) DEFAULT NULL,
  Pageviews       INT(11)      DEFAULT NULL,
  Platforms       VARCHAR(255) DEFAULT NULL,
  Reviewer        VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY (Id)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS MainDb.Roles;
CREATE TABLE MainDb.Roles (
  `Key`       VARCHAR(255) NOT NULL     COMMENT '角色',
  Name        VARCHAR(255) DEFAULT NULL COMMENT '名称',
  Description VARCHAR(255) DEFAULT NULL COMMENT '描述',
  PRIMARY KEY (`Key`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS MainDb.Routes;
CREATE TABLE MainDb.Routes (
  Id            INT(11)      NOT NULL     COMMENT '主键' AUTO_INCREMENT,
  ParentId      INT(11)      DEFAULT NULL COMMENT '父级Id',
  Name          VARCHAR(255) DEFAULT NULL COMMENT '名称',
  Path          VARCHAR(255) DEFAULT NULL COMMENT 'url路径',
  Component     VARCHAR(255) DEFAULT NULL COMMENT '文件位置',
  HiddenInt     INT(11)      DEFAULT NULL COMMENT '当设置 true 的时候该路由不会再侧边栏出现',
  Redirect      VARCHAR(255) DEFAULT NULL COMMENT '设置了面包屑的位置，当设置 noredirect 的时候该路由在面包屑导航中不可被点击',
  Roles         VARCHAR(255) DEFAULT NULL COMMENT '设置该路由进入的权限，多角色以,风格',
  Title         VARCHAR(255) DEFAULT NULL COMMENT '设置该路由在侧边栏和面包屑中展示的名字', 
  Icon          VARCHAR(255) DEFAULT NULL COMMENT '设置该路由的图标',
  BreadcrumbInt INT(11)      DEFAULT NULL COMMENT '如果设置为false，则不会在breadcrumb面包屑中显示',
  AffixInt      INT(11)      DEFAULT NULL COMMENT '设置true则固定再tag试图中不可删除',
  PRIMARY KEY (`Id`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS MainDb.RoleRoute;
CREATE TABLE MainDb.RoleRoute(
  Id            INT(11)      NOT NULL     COMMENT '主键' AUTO_INCREMENT,
  RoleKey       VARCHAR(255) NOT NULL     COMMENT '角色Key',
  RouteId       INT(11)      NOT NULL     COMMENT '路由Id',
  PRIMARY KEY (Id)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS MainDb.Transaction;
CREATE TABLE MainDb.Transaction(
  Id        INT(11)      NOT NULL     AUTO_INCREMENT,
  OrderNo   VARCHAR(255) DEFAULT NULL,
  Timestamp BIGINT(20)   DEFAULT NULL,
  Username  VARCHAR(255) DEFAULT NULL,
  Price     DEC(10, 10)  DEFAULT NULL,
  Status    VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS MainDb.UserInfo;
CREATE TABLE MainDb.UserInfo(
  Id           INT(11)      NOT NULL     AUTO_INCREMENT,
  RolesString  VARCHAR(255) DEFAULT NULL,
  Token        VARCHAR(255) DEFAULT NULL,
  Introduction VARCHAR(255) DEFAULT NULL,
  Avatar       VARCHAR(255) DEFAULT NULL,
  Name         VARCHAR(255) DEFAULT NULL,
  Username     VARCHAR(255) DEFAULT NULL,
  Password     VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
)
ENGINE = INNODB;
