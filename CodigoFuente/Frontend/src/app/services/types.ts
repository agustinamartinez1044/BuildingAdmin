export interface LoginModel {
  token: string;
}

export interface UserModel {
  name: string;
  email: string;
  role: string;
}

export interface InvitationModel {
  id: number;
  name: string;
  email: string;
  role: number;
  status: string;
  expirationDate: string;
}

export interface ConstructionCompanyModel {
  id: number;
  name: string;
}

export interface BuildingModel {
  id: number;
  name: string;
  address: string;
  location: string;
  managerName?: string;
  expenses: number;
  apartments: ApartmentModel[];
}

export interface ApartmentTicketModel {
  id?: number;
  doorNumber: number;
}

export interface ApartmentModel {
  id?: number;
  floor: number;
  doorNumber: number;
  ownerName: string;
  ownerLastName: string;
  ownerEmail: string;
  rooms: number;
  bathrooms: number;
  hasTerrace: boolean;
}

export interface CreateBuildingModel {
  name: string;
  address: string;
  location: string;
  expenses: number;
  apartments: ApartmentModel[];
  managerEmail: string;
}

export interface CreateBuildingOutputModel {
  id: number;
  name: string;
}

export interface ManagerModel {
  id: number;
  name: string;
  email: string;
  buildings: number[];
}

export interface ModifyApartmentModel {
  id: number;
  ownerName: string;
  ownerLastName: string;
  ownerEmail: string;
}

export interface ModifyBuildingModel {
  expenses: number;
  apartments: ModifyApartmentModel[];
}

export interface CompanyAdministratorModel {
  id: number;
  name: string;
  email: string;
  constructionCompanny: ConstructionCompanyModel;
}

export interface CreateCompanyAdministratorModel {
  name: string;
  email: string;
  password: string;
}

export interface ImporterInputModel {
  importerName: string;
  path: string;
}

export interface ImporterOutputModel {
  createdBuildings: CreateBuildingOutputModel[];
  errors: string[];
}

export interface Category {
  name: string;
  id: number;
  relatedTo: string | null;
}

export interface AssignedTo {
  lastName: string;
  buildings: BuildingModel[] | null;
  id: number;
  name: string;
  email: string;
  password: string;
}

export interface TicketModel {
  id: number;
  description: string;
  creationDate: string;
  apartment: ApartmentTicketModel;
  totalCost: number;
  createdBy: string | null;
  category: Category;
  status: string;
  attentionDate: string;
  closingDate: string;
  idOperatorAssignedTo: number | null;
}

export interface EditInvitationModel {
  id: number;
  expirationDate: string;
}

export interface TicketsByCategoriesModel {
  categoryName: string;
  ticketsOpen: number;
  ticketsInProgress: number;
  ticketsClosed: number;
}

export interface TicketsByBuildingModel {
  buildingName: string;
  ticketsOpen: number;
  ticketsInProgress: number;
  ticketsClosed: number;
}

export interface TicketsByApartmentsModel {
  apartmentAndOwner: string;
  ticketsOpen: number;
  ticketsInProgress: number;
  ticketsClosed: number;
}

export interface TicketsByMaintenanceOperatorModel {
  operatorName: string;
  ticketsOpen: number;
  ticketsInProgress: number;
  ticketsClosed: number;
  averageTimeToClose: string;
}

export interface CategoryModel {
  id: number;
  name: string;
  parentId: number | null;
}

export interface CreateCategoryModel {
  name: string;
}

export interface AdminModel {
  name: string;
  lastName: string;
  email: string;
  password: string;
}

export interface AcceptInvitationModel {
  email: string;
  password: string;
}

export interface RejectInvitationModel {
  email: string;
}

export interface AcceptInvitationOutputModel {
  userId: number;
}

export interface TicketCreateModel {
  description: string;
  apartmentId: number;
  categoryId: number;
}

export interface MaintenanceOperatorModel {
  name: string;
  lastName: string;
  email: string;
  password: string;
  buildings: number[];
}

export interface MaintenanceOperatorModelOut {
  id: number;
  name: string;
  lastName: string;
  email: string;
}